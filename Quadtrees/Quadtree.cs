/* -------------------------------------------------------------------------

    COIS 3020H Assignment #3
    Region Quadtrees

    Written by: S. Chapados
    April 2020

    QUADTREE CLASS

    FUNCTIONS:
        - Print
        - Switch
        - Union
        - Compare

    PRIVATE FUNCTIONS:
        - Construct (recursive constructor)
        - Traverse
        - Compress

   ------------------------------------------------------------------------- */

using System;
namespace Quadtrees
{
    public enum Colour { BLACK, WHITE, GRAY };

    public class Quadtree
    {
        class Node
        {
            public Colour value;    // Colour value at Node
            public int size;        // width of array rooted at node
            public Node nw;         // NW child
            public Node ne;         // NE child
            public Node sw;         // SW child
            public Node se;         // SE child
            public Node parent;     // parent node

            // Node
            // Creates an empty Node
            // Children and parent are set to null by default
            // Colour, Parent, and Size can be specified at time of creation
            // Time complexity:  O(1)

            public Node(Colour c = Colour.GRAY, Node p = null, int s = 0)
            {
                value = c; // default to Gray
                size = s;
                nw = null;
                ne = null;
                sw = null;
                se = null;
                parent = p;
            }
        }

        private Node root;

        // No-argument constructor used for union function
        private Quadtree()
        {
        }

        /* -------------------------------------------------------------------
           CONSTRUCTOR

           Creates a new quadtree from given image stored in 2D array.
        ------------------------------------------------------------------- */
        public Quadtree(Colour[,] img)
        {
            if (img.Length < 1) // original image is empty
            {
                root = new Node();
                return;
            }
            if (img.Length == 1) // original image is 1x1
            {
                root = new Node(img[0, 0], null, 1);
                return;
            }
            // else, recursively construct the tree
            root = Construct(img, Colour.GRAY, null);
        }
        /* -------------------------------------------------------------------
           PARAMETERS:
                img - image to be built into tree
                c - colour of above image
                p - parent node
        ------------------------------------------------------------------- */
        private Node Construct(Colour[,] img, Colour c, Node p)
        {
            int s = (int)Math.Sqrt(img.Length);
            Node n = new Node(c, p, s);

            if (c != Colour.GRAY) // if all one colour, no need to split
                return n;

            // else, split into four
            Colour[,] temp = new Colour[s / 2, s / 2];

            // NW quad
            c = img[0, 0];
            for (int i = 0; i < s / 2; ++i)
            {
                for (int j = 0; j < s / 2; ++j)
                {
                    temp[i, j] = img[i, j];
                    if (img[i, j] != c)
                        c = Colour.GRAY;
                }
            }
            n.nw = Construct(temp, c, n);

            // NE quad
            c = img[0, s / 2];
            for (int i = 0; i < s / 2; ++i)
            {
                for (int j = s / 2; j < s; ++j)
                {
                    temp[i, j - (s / 2)] = img[i, j];
                    if (img[i, j] != c)
                        c = Colour.GRAY;
                }
            }
            n.ne = Construct(temp, c, n);

            // SW quad
            c = img[s / 2, 0];
            for (int i = s / 2; i < s; ++i)
            {
                for (int j = 0; j < s / 2; ++j)
                {
                    temp[i - (s / 2), j] = img[i, j];
                    if (img[i, j] != c)
                        c = Colour.GRAY;
                }
            }
            n.sw = Construct(temp, c, n);

            // SE quad
            c = img[s / 2, s / 2];
            for (int i = s / 2; i < s; ++i)
            {
                for (int j = s / 2; j < s; ++j)
                {
                    temp[i - (s / 2), j - (s / 2)] = img[i, j];
                    if (img[i, j] != c)
                        c = Colour.GRAY;
                }
            }
            n.se = Construct(temp, c, n);
            Compress(n.nw); // compress starting from any child
            return n;
        }


        /* -------------------------------------------------------------------
           FUNCTION: Print

           Calls recursive function below to traverse the tree andvcreate a
           matrix, then prints the values.

           Time Complexity: O(n), n = number of pixels in image
        ------------------------------------------------------------------- */
        public void Print()
        {
            int s = root.size; // width & length of matrix
            char[,] M = new char[s, s]; // matrix to be printed
            if (s > 0)
                Traverse(M, 0, 0, root); // traverse the tree

            // this part prints the end result
            for (int i = 0; i < s; ++i)
            {
                for (int j = 0; j < s; ++j)
                {
                    Console.Write(M[i, j] + " ");
                }
                Console.Write("\n");
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Traverse (private)

           Traverses the tree and stores values in 2D char array.
           Used by Print and Compare functions.

           PARAMETERS:
                tree - 2D char array where values will be inserted
                i, j - start indexes to insert values into tree
                p - current node / node to start at
        ------------------------------------------------------------------- */
        private void Traverse(char[,] M, int i, int j, Node p)
        {
            int s = p.size;
            switch (p.value)
            {
                case Colour.GRAY:
                    Traverse(M, i, j, p.nw); // NW
                    Traverse(M, i, j + (s / 2), p.ne); // NE
                    Traverse(M, i + (s / 2), j, p.sw); // SW
                    Traverse(M, i + (s / 2), j + (s / 2), p.se); // SE
                    break;
                default: // leaf node
                    char c;
                    if (p.value == Colour.BLACK)
                        c = 'B';
                    else
                        c = 'W';
                    for (int k = i; k < i + s; ++k)
                        for (int n = j; n < j + s; ++n)
                            M[k, n] = c;
                    break;
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Compress

           Compresses tree to minimize number of nodes. If all children of a
           node are same colour, makes node that colour and erases children.

           PARAMETER:
                p - leaf node to start at

           Time Complexity: O(k), k = depth of the tree
        ------------------------------------------------------------------- */
        private void Compress(Node p)
        {
            while (p.parent != null) // stop at root
            {
                p = p.parent;
                if (p.nw != null && p.ne != null && p.sw != null && p.se != null)
                {
                    // if all children are black
                    if (p.nw.value == Colour.BLACK && p.ne.value == Colour.BLACK &&
                            p.sw.value == Colour.BLACK && p.se.value == Colour.BLACK)
                        p.value = Colour.BLACK;
                    // if all children are white
                    else if (p.nw.value == Colour.WHITE && p.ne.value == Colour.WHITE &&
                            p.sw.value == Colour.WHITE && p.se.value == Colour.WHITE)
                        p.value = Colour.WHITE;
                    else // not all children are same
                        return;

                    p.nw = null;
                    p.ne = null;
                    p.sw = null;
                    p.se = null;
                }
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Switch

           Switches colour at index [i, j] from White to Black or vice versa.

           Time Complexity: O(k), k = depth of the tree
        ------------------------------------------------------------------- */
        public void Switch(int i, int j)
        {
            if (i >= root.size || j >= root.size) // make sure indices are valid
                return;
            Switch(i, j, root);
        }
        private void Switch(int i, int j, Node p)
        {
            if (p.value == Colour.GRAY) // if not a leaf node
            {
                if (i < p.size / 2) // North
                {
                    if (j < p.size / 2) // NW
                        Switch(i, j, p.nw);
                    else // NE
                        Switch(i, j - (p.size/2), p.ne);
                }
                else // South
                {
                    if (j < p.size / 2) // SW
                        Switch(i - (p.size / 2), j, p.sw);
                    else // SE
                        Switch(i - (p.size / 2), j - (p.size/2), p.se);
                }
            }
            else // leaf node reached
            {
                if (p.size == 1) // if 1 pixel, swap colour and compress
                {
                    if (p.value == Colour.BLACK)
                        p.value = Colour.WHITE;
                    else
                        p.value = Colour.BLACK;
                    Compress(p);
                }
                else // split node
                {
                    p.nw = new Node(p.value, p, p.size / 2);
                    p.ne = new Node(p.value, p, p.size / 2);
                    p.sw = new Node(p.value, p, p.size / 2);
                    p.se = new Node(p.value, p, p.size / 2);
                    p.value = Colour.GRAY;
                    if (i < p.size / 2) // North
                    {
                        if (j < p.size / 2) // NW
                            Switch(i, j, p.nw);
                        else // NE
                            Switch(i, j - (p.size / 2), p.ne);
                    }
                    else // South
                    {
                        if (j < p.size / 2) // SW
                            Switch(i - (p.size / 2), j, p.sw);
                        else // SE
                            Switch(i - (p.size / 2), j - (p.size / 2), p.se);
                    }
                }
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Union

           Combines two quadtrees into one.
           A pixel is:
                Black when either is black
                White when both are white

           Time Complexity: O(n), n = number of nodes in deeper tree
        ------------------------------------------------------------------- */
        public static Quadtree Union(Quadtree T1, Quadtree T2)
        {
            Quadtree T = new Quadtree();
            int s = Math.Max(T1.root.size, T2.root.size);
            T.root = Union(T1.root, T2.root, null, s);
            return T;
        }
        private static Node Union(Node r1, Node r2, Node p, int s)
        {
            Node r;
            // both white
            if (r1.value == Colour.WHITE && r2.value == Colour.WHITE)
                r = new Node(Colour.WHITE, p, s);
            // either one black
            else if (r1.value == Colour.BLACK || r2.value == Colour.BLACK)
                r = new Node(Colour.BLACK, p, s);
            // none black, at least one gray
            else
            {
                r = new Node(Colour.GRAY, p, s);
                s /= 2;
                if (r1.value == Colour.WHITE)
                {
                    r.nw = Union(r1, r2.nw, r, s);
                    r.ne = Union(r1, r2.ne, r, s);
                    r.sw = Union(r1, r2.sw, r, s);
                    r.se = Union(r1, r2.se, r, s);
                }
                else if (r2.value == Colour.WHITE)
                {
                    r.nw = Union(r1.nw, r2, r, s);
                    r.ne = Union(r1.ne, r2, r, s);
                    r.sw = Union(r1.sw, r2, r, s);
                    r.se = Union(r1.se, r2, r, s);
                }
                else
                {
                    r.nw = Union(r1.nw, r2.nw, r, s);
                    r.ne = Union(r1.ne, r2.ne, r, s);
                    r.sw = Union(r1.sw, r2.sw, r, s);
                    r.se = Union(r1.se, r2.se, r, s);
                }
            }
            return r;
        }


        /* -------------------------------------------------------------------
           FUNCTION: Compare

           Compares an image (stored in 2d matrix) to the quadtree.
           RETURNS: true if both images are identical, false otherwise.

           Time Complexity: O(n), n = number of entries in matrix
        ------------------------------------------------------------------- */
        public bool Compare(Colour[,] img)
        {
            if ((int)Math.Sqrt(img.Length) != root.size)
                return false; // images are different sizes

            bool result = true;
            int s = root.size;
            char[,] M = new char[s, s];
            if (s > 0)
                Traverse(M, 0, 0, root);
            for (int i = 0; i < s; ++i)
            {
                for (int j = 0; j < s; ++j)
                {
                    switch (img[i, j])
                    {
                        case Colour.WHITE:
                            if (M[i, j] != 'W')
                                result = false;
                            break;
                        default:
                            if (M[i, j] != 'B')
                                result = false;
                            break;
                    }
                }
            }
            return result;
        }
    }
}
