/* -------------------------------------------------------------------------

    COIS 3020H Assignment #2
    Part B: Ropes

    Written by: S. Chapados
    February/March 2020

    ROPE CLASS

    FUNCTIONS:
        - Split
        - Insert
        - Delete
        - Substring
        - CharAt
        - Print
        - Concatenate

    PRIVATE FUNCTIONS:
        - Add
        - NodeAt
        - ReBalance

   ------------------------------------------------------------------------- */

using System;
namespace Ropes
{
    public class Rope
    {
        private Node root;
        private int TotalLength // length of string stored in rope - READ ONLY
        {
            get
            {
                if (root.left == null && root.right == null)
                    return root.length;
                if (root.left.right == null)
                    return root.left.length + root.right.length;
                return root.length + root.left.length + root.right.length;
            }
        }

        class Node
        {
            public string value;    // Value at Node
            public int length;      // length of string in left subtree
            public Node left;       // left child
            public Node right;      // right child
            public Node parent;     // parent node

            // Node
            // Creates an empty Node
            // Children and parent are set to null by default
            // Time complexity:  O(1)

            public Node()
            {
                value = null;
                length = 0;
                left = null;
                right = null;
                parent = null;
            }
        }


        /* -------------------------------------------------------------------
           CONSTRUCTOR

           Creates a new rope from a given string. Calls Add function below
           to recursively add substrings.
        ------------------------------------------------------------------- */
        public Rope(string s)
        {
            root = new Node();

            // split s into strings of no more than 10 chars

            if (s.Length <= 10) // no need to split
            {
                root.value = s;
                root.length = s.Length;
            }
            else // divide and conquer
            {
                root.left = new Node
                { parent = root };
                root.right = new Node
                { parent = root };
                // first half of string goes to the left
                Add(root.left, s.Substring(0, s.Length / 2));
                // second half goes on the right
                Add(root.right, s.Substring(s.Length / 2));

                // calculate length
                Node cur = root.left;
                root.length = cur.length;
                while (cur.right != null)
                {
                    root.length += cur.right.length;
                    cur = cur.right;
                }
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Add (private)

           Recursively splits string until length is <= 10, then adds to
           a leaf node.
        ------------------------------------------------------------------- */
        private void Add(Node r, string s)
        {
            if (s.Length <= 10) // add string to current node
            {
                r.value = s;
                r.length = s.Length;
            }
            else // split current node
            {
                r.left = new Node
                { parent = r };
                r.right = new Node
                { parent = r };
                // first half of string goes to the left
                Add(r.left, s.Substring(0, s.Length / 2));
                // second half goes on the right
                Add(r.right, s.Substring(s.Length / 2));

                // calculate length
                Node cur = r.left;
                r.length = cur.length;
                while (cur.right != null)
                {
                    r.length += cur.right.length;
                    cur = cur.right;
                }
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Split

           Splits a rope into two at index i, returns second rope.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        public Rope Split(int i)
        {
            if (i == 0) // no split, return copy of original rope
                return this;

            Rope R2 = new Rope(""); // rope to be returned
            Rope Rtemp = new Rope(""); // temp - used for concatenation to R2
            Node p = NodeAt(ref i); // find leaf node containing i
            Node cur; // used to traverse up tree

            if (p != null)
            {
                if (i > 0) // i is not at start of node
                {
                    // split p into two nodes
                    p.left = new Node()
                    {
                        value = p.value.Substring(0, i),
                        parent = p,
                        length = p.value.Substring(0, i).Length
                    };
                    p.right = new Node()
                    {
                        value = p.value.Substring(i),
                        parent = p,
                        length = p.value.Substring(i).Length
                    };
                    p.value = null;
                    p.length = p.left.length;
                    p = p.right; // set p to right child
                }

                // move up tree until you're at a right child or root
                while (p != root && p == p.parent.left)
                    p = p.parent;

                cur = p.parent; // keep track of parent node
                p.parent = null; // remove link to parent
                R2.root = p; // set p to root of new rope
                cur.right = null; // cut off node p

                // move up tree until you have a new right child
                while (cur != root && cur == cur.parent.right)
                    cur = cur.parent;
                cur = cur.parent;

                // continue cutting off right children
                while (cur != null && cur.right != null)
                {
                    cur.right.parent = null;
                    Rtemp.root = cur.right;
                    R2.Concatenate(Rtemp);
                    cur.right = null;

                    // stop loop at right child of root or at root
                    if (cur != root.right && cur != root)
                    {
                        // find new right child
                        while (cur != root && cur == cur.parent.right)
                            cur = cur.parent;
                        if (cur == root)
                            cur = null;
                    }
                }
                ReBalance(); // Rebalance the tree starting from root
            }
            return R2;
        }


        /* -------------------------------------------------------------------
           FUNCTION: Insert

           Insert string s at index i.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        public void Insert(string s, int i)
        {
            Rope S = new Rope(s);

            if (i >= TotalLength) // add s to end
                Concatenate(S);
            else if (i <= 0) // add s to beginning
            {
                S.Concatenate(this);
                root = S.root;
            }
            else // split rope
            {
                Rope R2 = Split(i);
                Concatenate(S);
                Concatenate(R2);
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Delete

           Deletes the substring between indexes i-j.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        public void Delete(int i, int j)
        {
            if (i < 0 || j < 0 || i > j) // if indices are invalid, return
                return;

            Rope R2;
            if (j >= TotalLength) // cut off end of rope
                R2 = Split(i);
            else
            {
                R2 = Split(i); // split at i
                R2 = R2.Split(j - i + 1); // split at j
                Concatenate(R2); // piece remains together
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Substring

           Returns the substring between indexes i-j.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        public string Substring(int i, int j)
        {
            string s = "";
            Node cur;
            int l; // length of substring

            if (j >= TotalLength) // if j is too big, fix it
                j = TotalLength - 1;

            if (i < 0 || j < 0 || i > j) // if indices are invalid, return
                return s;

            if (i == j) // this is just one char
                return CharAt(i).ToString();

            l = j - i + 1; // set length

            while (l > 0)
            {
                cur = NodeAt(ref i); // find index i
                if (cur.length - i < l) // substring spans multiple nodes
                {
                    l += s.Length;
                    s += cur.value.Substring(i);

                    // if left child, move up to parent
                    if (cur == cur.parent.left)
                        cur = cur.parent;
                    else
                    {
                        // if parent is a left child, move to grandparent
                        if (cur.parent == cur.parent.parent.left)
                            cur = cur.parent.parent;
                        // if parent is also a right child, move to root
                        else
                            cur = root;
                    }
                    i = cur.length; // set i to next index in string
                    l -= s.Length; // subtract length that was added
                }
                else // substring is contained entirely in this node
                {
                    s += cur.value.Substring(i, l);
                    l = 0; // end loop
                }
            }
            return s;
        }


        /* -------------------------------------------------------------------
           FUNCTION: CharAt

           Returns the character at index i.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        public char CharAt(int i)
        {
            char ch = ' '; // returns blank if index not found
            Node p = NodeAt(ref i); // find leaf node containing i
            if (p != null)
                ch = p.value[i];
            return ch;
        }


        /* -------------------------------------------------------------------
           FUNCTION: Print

           Calls recursive Print function below, which then traverses the
           tree and prints the value of each leaf node.

           Time Complexity: O(N), N = number of nodes in tree
        ------------------------------------------------------------------- */
        public void Print()
        {
            Print(root); // call recursive function starting with root
        }
        private void Print(Node r)
        {
            if (r == null)
                return;

            if (r.left == null && r.right == null) // leaf node reached
                Console.Write(r.value); // print value
            else
            {
                Print(r.left);
                Print(r.right);
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: Concatennate

           Concatenates R2 onto the end of calling Rope.

           Time Complexity: O(1) + O(logN)
        ------------------------------------------------------------------- */
        public void Concatenate(Rope R2)
        {
            // create new root node
            Node r = new Node
            {
                left = root,
                right = R2.root
            };
            r.left.parent = r;
            r.right.parent = r;

            // calculate new root length
            r.length = r.left.length;
            Node cur = r.left;
            while (cur.right != null)
            {
                r.length += cur.right.length;
                cur = cur.right;
            }
            root = r; // overwrite old root
        }


        /* -------------------------------------------------------------------
           FUNCTION: ReBalance (private)

           Rebalances the tree starting from root, called after a split.

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        private void ReBalance()
        {
            Node cur;
            while (root.right == null && root.left != null)
            {
                root.left.parent = null;
                root = root.left;
            }

            cur = root.right;
            while (cur != null && cur.right == null && cur.left != null)
            {
                cur.left.parent = cur.parent;
                cur.parent.right = cur.left;
                cur = cur.left;
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: NodeAt (private)

           Returns the leaf node containing a given index i.
           Also adjusts i in relation to the substring within its node, so
           can be used within calling function (see CharAt, for example).

           Time Complexity: O(logN)
        ------------------------------------------------------------------- */
        private Node NodeAt(ref int i)
        {
            Node cur = root;

            if (i >= 0 && i < TotalLength) // make sure index is in tree
            {
                while (cur.left != null || cur.right != null)
                {
                    // search left subtree
                    if (i < cur.length)
                        cur = cur.left;

                    // search right subtree
                    else
                    {
                        i -= cur.length;
                        cur = cur.right;
                    }
                }
                return cur;
            }
            return null; // if i is not in tree
        }


        /* -------------------------------------------------------------------
           BREADTH-FIRST SEARCH FUNCTIONS

           Included for testing purposes only.
           Returns a string containing the lengths of each node.

           Written by me in Summer 2019.
        ------------------------------------------------------------------- */
        public string BreadthFirst()
        {
            return BreadthFirst(root);
        }
        private string BreadthFirst(Node root)
        {
            string s = "";
            int h = Height(root);
            for (int i = 1; i <= h; ++i)
            {
                s += GetLevel(root, i);
            }
            return s;
        }
        private string GetLevel(Node root, int level)
        {
            if (root != null)
            {
                if (level == 1)
                    return root.length.ToString() + " ";
                if (level > 1)
                    return GetLevel(root.left, level - 1)
                        + GetLevel(root.right, level - 1);
            }
            return "";
        }
        private int Height(Node root)
        {
            if (root == null)
                return 0;
            else
            {
                int lHeight = Height(root.left);
                int rHeight = Height(root.right);
                if (lHeight > rHeight)
                    return lHeight + 1;
                else
                    return rHeight + 1;
            }
        }
    }
}
