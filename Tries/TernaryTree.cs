/* -------------------------------------------------------------------------

    COIS 3020H Assignment #2
    Part A: Trie, Trie Again

    Written by: S. Chapados
    February/March 2020

    TERNARY TREE CLASS

    Functions:
        - Partial Match
        - Autocomplete

    (other functions written by Professor Brian Patrick at Trent University)

------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tries
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    //-------------------------------------------------------------------------

    public interface ITrie<T> : IContainer<T>
    {
        bool Insert(string key, T value);
        T Value(string key);
    }

    //-------------------------------------------------------------------------

    class TrieTernary<T> : ITrie<T>
    {
        private Node root;                 // Root node of the Trie
        private int size;                  // Number of values in the Trie

        class Node
        {
            public char ch;                // Character of the key
            public T value;                // Value at Node; otherwise default
            public Node low, middle, high; // Left, middle, and right subtrees

            // Node
            // Creates an empty Node
            // All children are set to null
            // Time complexity:  O(1)

            public Node(char ch)
            {
                this.ch = ch;
                value = default(T);
                low = middle = high = null;
            }
        }


        /* -------------------------------------------------------------------
           FUNCTION: PartialMatch

           Finds all keys that match a given pattern, such as ".a.d" where '.' is
           a wildcard and 'a' - 'z' are specific characters (not case-sensitive).

           PARAMETERS:
               pattern - string containing pattern to be matched

           RETURNS: List of strings containing all matching keys
        ------------------------------------------------------------------- */
        public List<string> PartialMatch(string pattern)
        {
            List<string> s = new List<string>();
            Node p = root;
            string key = "";
            pattern = pattern.ToLower(); // ignore uppercase characters

            for (int i = 0; i < pattern.Length; ++i)
            {
                if (pattern[i] == '.') // wildcard
                {
                    // search left subtree for matching keys
                    if (p.low != null)
                    {
                        if (i == pattern.Length - 1) // wildcard at end of pattern
                            s.AddRange(PartialMatch(p.low, "", key));
                        else
                            s.AddRange(PartialMatch(p.low, pattern, key));
                    }
                    // search right subtree for matching keys
                    if (p.high != null)
                    {
                        if (i == pattern.Length - 1) // wildcard at end of pattern
                            s.AddRange(PartialMatch(p.high, "", key));
                        else
                            s.AddRange(PartialMatch(p.high, pattern, key));
                    }
                    // search middle subtree for matching keys
                    if (i < pattern.Length && p.middle != null)
                    {
                        key += p.ch;
                        s.AddRange(PartialMatch(p.middle, pattern.Substring(++i), key));
                    }
                }
                else
                {
                    while (p != null)
                    {
                        // Search for current character of the pattern in left subtree
                        if (pattern[i] < p.ch)
                            p = p.low;
                        // Search for current character of the pattern in right subtree
                        else if (pattern[i] > p.ch)
                            p = p.high;
                        else // if (p.ch == pattern[i])
                        {
                            key += p.ch;
                            if (i < pattern.Length - 1) // not at end of pattern
                                p = p.middle; // continue down middle subtree
                            break;
                        }
                    }
                    if (p == null) // pattern not found
                        break;
                }
            }

            // if key found, add to list
            if (p != null && !p.value.Equals(default(T)))
            {
                s.Add(key);
            }

            return s;
        }


        /* -------------------------------------------------------------------
           FUNCTION: PartialMatch (private)

           Recursive function to find keys matching a particular pattern -
           called on wildcard characters (nearly identical to above function)

           PARAMETERS:
               p - Node containing current root
               pattern - string containing pattern to be matched
               key - string containing the current key so far

           RETURNS: List of strings containing all matching keys
        ------------------------------------------------------------------- */
        private List<string> PartialMatch(Node p, string pattern, string key)
        {
            List<string> s = new List<string>();

            for (int i = 0; i < pattern.Length; ++i)
            {
                if (pattern[i] == '.') // wildcard
                {
                    // search left subtree for matching keys
                    if (p.low != null)
                    {
                        if (i == pattern.Length - 1) // wildcard at end of pattern
                            s.AddRange(PartialMatch(p.low, "", key));
                        else
                            s.AddRange(PartialMatch(p.low, pattern, key));
                    }
                    // search right subtree for matching keys
                    if (p.high != null)
                    {
                        if (i == pattern.Length - 1) // wildcard at end of pattern
                            s.AddRange(PartialMatch(p.high, "", key));
                        else
                            s.AddRange(PartialMatch(p.high, pattern, key));
                    }
                    // search middle subtree for matching keys
                    if (i < pattern.Length && p.middle != null)
                    {
                        key += p.ch;
                        s.AddRange(PartialMatch(p.middle, pattern.Substring(++i), key));
                    }
                }
                else
                {
                    while (p != null)
                    {
                        // Search for current character of the pattern in left subtree
                        if (pattern[i] < p.ch)
                            p = p.low;
                        // Search for current character of the pattern in right subtree
                        else if (pattern[i] > p.ch)
                            p = p.high;
                        else // if (p.ch == pattern[i])
                        {
                            key += p.ch;
                            if (i < pattern.Length - 1) // not at end of pattern
                                p = p.middle; // continue down middle subtree
                            break;
                        }
                    }
                    if (p == null) // pattern not found
                        break;
                }
            }

            // if key found, add to list
            if (p != null && !p.value.Equals(default(T)))
            {
                key += p.ch;
                s.Add(key);
            }

            return s;
        }


        /* -------------------------------------------------------------------
           FUNCTION: Autocomplete

           Finds all keys that begin with a specified prefix.

           PARAMETERS:
               prefix - string containing prefix to be matched

           RETURNS: List of strings containing all matching keys
        ------------------------------------------------------------------- */
        public List<string> Autocomplete(string prefix)
        {
            List<string> s = new List<string>();
            int i = 0;
            Node p = root;
            string key = prefix.ToLower();

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    if (++i == key.Length) // all characters of prefix have been visited
                        break;
                    // Move to next character of the key in the middle subtree
                    p = p.middle;
                }
            }

            // if prefix is a key, add to list
            if (p != null && !p.value.Equals(default(T)))
                s.Add(key);

            // find remaining keys with new root
            if (p != null && p.middle != null)
                s.AddRange(Autocomplete(p.middle, key));

            return s;
        }


        /* -------------------------------------------------------------------
            FUNCTION: Autocomplete (private)

            Recursive function to find all keys from a given root (prefix).

            PARAMETERS:
                p - Node containing current root
                prefix - string containing prefix to be matched

            RETURNS: List of strings containing all matching keys
         ------------------------------------------------------------------- */
        private List<string> Autocomplete(Node p, string prefix)
        {
            List<string> s = new List<string>();
            string key = prefix.ToLower();

            // search for keys in left subtree
            if (p.low != null)
                s.AddRange(Autocomplete(p.low, key));

            // search for keys in right subtree
            if (p.high != null)
                s.AddRange(Autocomplete(p.high, key));

            key += p.ch; // add current char to key

            // if prefix is a key, add to list
            if (!p.value.Equals(default(T)))
                s.Add(key);

            // search middle subtree for additional keys
            if (p.middle != null)
                s.AddRange(Autocomplete(p.middle, key));

            return s;
        }

        /* -------------------------------------------------------------------------
            COIS 3020H Assignment #2 Part A: Trie, Trie Again

            END
        ------------------------------------------------------------------------- */


        // Trie
        // Creates an empty Trie
        // Time complexity:  O(1)

        public TrieTernary ()
        {
            MakeEmpty();
            size = 0;
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(ref root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored

        private bool Insert(ref Node p, string key, int i, T value)
        {
            if (p == null)
                p = new Node(key[i]);

            // Current character of key inserted in left subtree
            if (key[i] < p.ch)
                return Insert(ref p.low, key, i, value);

            // Current character of key inserted in right subtree
            else if (key[i] > p.ch)
                return Insert(ref p.high, key, i, value);

            else if (i + 1 == key.Length)
            // Key found
            {
                // But key/value pair already exists
                if (!p.value.Equals(default(T)))
                    return false;
                else
                {
                    // Place value in node
                    p.value = value;
                    size++;
                    return true;
                }
            }

            else
                // Next character of key inserted in middle subtree
                return Insert(ref p.middle, key, i + 1, value);
        }

        // Value
        // Returns the value associated with a key; otherwise default

        public T Value(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return the value if all characters of the key have been visited
                    if (++i == key.Length)
                        return p.value;

                    // Move to next character of the key in the middle subtree
                    p = p.middle;
                }
            }
            return default(T);   // Key too long
        }

        // Contains
        // Returns true if the given key is found in the Trie; false otherwise

        public bool Contains(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return true if the key is associated with a non-default value; false otherwise
                    if (++i == key.Length)
                        return !p.value.Equals(default(T));

                    // Move to next character of the key in the middle subtree
                    p = p.middle;
                }
            }
            return false;        // Key too long
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = null;
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root == null;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root,"");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys

        private void Print(Node p, string key)
        {
            if (p != null)
            {
                Print(p.low, key);
                if (!p.value.Equals(default(T)))
                    Console.WriteLine(key + p.ch + " " + p.value);
                Print(p.middle, key+p.ch);
                Print(p.high, key);
            }
        }
    }
}
