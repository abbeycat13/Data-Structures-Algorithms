/* -------------------------------------------------------------------------

    COIS 3020H Assignment #2
    Part A: Trie, Trie Again

    Written by: S. Chapados
    February/March 2020

    R-WAY TREE CLASS

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
    class TrieRWay<T> : ITrie<T>
    {
        private Node root;          // Root node of the Trie

        class Node
        {
            public T value;         // Value at Node; otherwise default
            public int numValues;   // Number of descendent values of a Node
            public Node[] child;    // Branching for each letter 'a' .. 'z'

            // Node
            // Creates an empty Node
            // All children are set to null by default
            // Time complexity:  O(1)

            public Node()
            {
                value = default(T);
                numValues = 0;
                child = new Node[26];
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
            int index;
            int l = pattern.Length;
            string key = "";

            for (int i = 0; i < pattern.Length; ++i)
            {
                if (pattern[i] == '.') // wildcard
                {
                    // search every non-null child for matching pattern
                    for (int j = 0; j < p.child.Length; ++j)
                    {
                        if (p.child[j] != null)
                        {
                            key += Convert.ToChar(j + 'a');
                            if (i == pattern.Length - 1) // wildcard is at end of string
                                s.AddRange(PartialMatch(p.child[j], "", key, l));
                            else
                                s.AddRange(PartialMatch(p.child[j], pattern.Substring(i + 1), key, l));
                            key = key.Substring(0, key.Length - 1);
                        }
                    }
                }
                else
                {
                    index = Char.ToLower(pattern[i]) - 'a';

                    if (p.child[index] == null)
                        return s;    // no keys with this pattern
                    else
                    {
                        key += pattern[i];
                        p = p.child[index];
                    }
                }
            }

            if (!p.value.Equals(default(T)) && key.Length == l)
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
               l - int storing length of original pattern

           RETURNS: List of strings containing all matching keys
        ------------------------------------------------------------------- */
        private List<string> PartialMatch(Node p, string pattern, string key, int l)
        {
            List<string> s = new List<string>();
            int index;

            for (int i = 0; i < pattern.Length; ++i)
            {
                if (pattern[i] == '.') // wildcard
                {
                    // search every non-null child for matching pattern
                    for (int j = 0; j < p.child.Length; ++j)
                    {
                        if (p.child[j] != null)
                        {
                            key += Convert.ToChar(j + 'a');
                            if (i == pattern.Length - 1) // wildcard is at end of string
                                s.AddRange(PartialMatch(p.child[j], "", key, l));
                            else
                                s.AddRange(PartialMatch(p.child[j], pattern.Substring(i + 1), key, l));
                            key = key.Substring(0, key.Length-1);
                        }
                    }
                }
                else
                {
                    index = Char.ToLower(pattern[i]) - 'a';

                    if (p.child[index] == null)
                        return s;    // no keys with this pattern
                    else
                    {
                        key += pattern[i];
                        p = p.child[index];
                    }
                }
            }

            if (!p.value.Equals(default(T)) && key.Length == l)
            {
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
            int i;
            Node p = root;
            int numKeys;
            string key = prefix;

            // find number of keys with this prefix
            foreach (char ch in prefix)
            {
                i = Char.ToLower(ch) - 'a';
                if (p.child[i] == null)
                    return s;    // no keys with this prefix
                else
                    p = p.child[i];
            }
            numKeys = p.numValues;

            // if the prefix is a key, add to list
            if (!p.value.Equals(default(T)))
            {
                s.Add(key);
                --numKeys;
            }

            // find all of the remaining keys
            if (numKeys > 0)
			{
                key = prefix;
                for (int j = 0; j < p.child.Length; ++j)
				{
                    // search every non-null child for keys
                    if (p.child[j] != null)
					{
                        key += Convert.ToChar(j + 'a');
                        s.AddRange(Autocomplete(p.child[j], key));
                        key = prefix;
					}
				}

			}
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
            int numKeys = p.numValues; // number of keys rooted at this node
            string key = prefix;

            // if the prefix is a key, add to list
            if (!p.value.Equals(default(T)))
            {
                s.Add(key);
                --numKeys;
            }

            // find all remaining keys
            if (numKeys > 0)
            {
                key = prefix;
                for (int j = 0; j < p.child.Length; ++j)
                {
                    // search every non-null child for keys
                    if (p.child[j] != null)
                    {
                        key += Convert.ToChar(j + 'a');
                        s.AddRange(Autocomplete(p.child[j], key));
                        key = prefix;
                    }
                }
            }
            return s;
        }

        /* -------------------------------------------------------------------------
            COIS 3020H Assignment #2 Part A: Trie, Trie Again

            END
        ------------------------------------------------------------------------- */


        // Trie
        // Creates an empty Trie
        // Time complexity:  O(1)

        public TrieRWay()
        {
            MakeEmpty();
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored
        // Time complexity:  O(L) where L is the length of the key

        private bool Insert(Node p, string key, int j, T value)
        {
            int i;

            if (j == key.Length)
            {
                if (p.value.Equals(default(T)))
                {
                    // Sets the value at the Node
                    p.value = value;
                    p.numValues++;
                    return true;
                }
                // Duplicate keys are ignored (unsuccessful insertion)
                else
                    return false;
            }
            else
            {
                // Maps a character to an index
                i = Char.ToLower(key[j]) - 'a';

                // Creates a new Node if the link is null
                // Note: Node is initialized to the default value
                if (p.child[i] == null)
                    p.child[i] = new Node();

                // If the inseration is successful
                if (Insert(p.child[i], key, j + 1, value))
                {
                    // Increase number of descendent values by one
                    p.numValues++;
                    return true;
                }
                else
                    return false;
            }
        }

        // Value
        // Returns the value associated with a key; otherwise default
        // Time complexity:  O(L) where L is the length of the key

        public T Value(string key)
        {
            int i;
            Node p = root;

            // Traverses the links character by character
            foreach (char ch in key)
            {
                i = Char.ToLower(ch) - 'a';
                if (p.child[i] == null)
                    return default(T);    // Key is too long
                else
                    p = p.child[i];
            }
            return p.value;               // Returns the value or default
        }

        // Public Remove
        // Calls the private Remove that carries out the actual deletion
        // Returns true if successful; false otherwise

        public bool Remove(string key)
        {
            return Remove(root, key, 0);
        }

        // Private Remove
        // Removes the value associated with the given key
        // Time complexity:  O(L) where L is the length of the key

        private bool Remove(Node p, string key, int j)
        {
            int i;

            // Key not found
            if (p == null)
                return false;

            else if (j == key.Length)
            {
                // Key/value pair found
                if (!p.value.Equals(default(T)))
                {
                    p.value = default(T);
                    p.numValues--;
                    return true;
                }
                // No value with associated key
                else
                    return false;
            }

            else
            {
                i = Char.ToLower(key[j]) - 'a';

                // If the deletion is successful
                if (Remove(p.child[i], key, j + 1))
                {
                    // Decrease number of descendent values by one and
                    // Remove Nodes with no remaining descendents
                    if (p.child[i].numValues == 0)
                        p.child[i] = null;
                    p.numValues--;
                    return true;
                }
                else
                    return false;
            }
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = new Node();
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root.numValues == 0;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return root.numValues;
        }

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root,"");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys
        // Time complexity:  O(S) where S is the total length of the keys

        private void Print(Node p, string key)
        {
            int i;

            if (p != null)
            {
                //if (!p.value.Equals(default(T)))
                Console.WriteLine(key + " " + p.value + " " + p.numValues);
                for (i = 0; i < 26; i++)
                    Print(p.child[i], key+(char)(i+'a'));
            }
        }
    }
}
