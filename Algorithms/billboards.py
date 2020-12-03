#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Tue Mar 17 03:46:38 2020

COIS 4050H: Assignment #2 (Part A), Question 3
@author: S. Chapados

BILLBOARDS PROGRAM FILE
"""

"""
    FUNCTION: Billboards
    
    Calculates maximum monthly revenue for i billboard sites.
    
    Recurrence Relation: 
        M[i] = M[i-1] + R[i]               if D[i]-D[i-1] > m
               max(M[i-1], M[j] + R[i])    if D[i]-D[j] > m
               max(M[i-1], R[i])           else    
    Initial Condition: 
        M[0] = R[0]
    
    Time Complexity: O(n)
"""
def Billboards(D, R, m):
    print("\n\nDistances: " + str(D))
    print("Revenue: " + str(R))
    print("No billboards within " + str(m) + " miles")
    
    if len(D) < 1 or len(D) != len(R) or m < 0: # don't let program crash
        print("Invalid input")
        return
    
    """  TABLE BUILDING: O(n)  """  
    M = [R[0]] # max revenue for first i sites
    S = [[D[0]]] # where to place billboards for max revenue
    j = 0 # index of farthest site that can be <= m away
    
    for i in range(1, len(D)):
        # can have both i and i-1
        if (D[i]-D[i-1]) > m: 
            M.append(M[i-1] + R[i])
            S.append(S[i-1] + [D[i]])
        # can and should have i and j
        elif (D[i] - D[j]) > m and (M[j] + R[i]) >= M[i-1]: 
            M.append(M[j] + R[i])
            S.append(S[j] + [D[i]])
            j = i
        # can't have i and j
        elif R[i] >= M[i-1]: 
            M.append(R[i])
            S.append([D[i]])  
        # when i-1 produces more revenue in any case
        else: 
            M.append(M[i-1])
            S.append(S[i-1])

    """  OPTIMAL SOLUTION: O(n)  """
    for k in range(len(D)):
        print("\nSOLUTION FOR " + str(k+1) + " SITES:" +
              "\nRevenue: " + str(M[k]) +
              "\nPlace Billboards at: " + str(S[k]))


"""  TEST CASES  """
D0 = [4, 5, 6, 7, 10, 11] # distances of billboard sites
R0 = [20, 15, 35, 30, 50, 60] # revenue for each site
m0 = 2 # no billboards can be within m miles of each other

D1 = [1, 2, 3, 4]
R1 = [10, 20, 30, 40]
m1 = 6

D2 = [3, 6, 9, 12]
R2 = [5, 15, 10, 20]
m2 = 2

D3 = [3, 6, 7, 10, 13, 14]
R3 = [5, 15, 10, 5, 15, 10]
m3 = 2

D4 = [50, 65, 90, 100]
R4 = [35, 15, 10, 20]
m4 = 20

D5 = [3, 4, 5, 6]
R5 = [5, 15, 10, 40]
m5 = 0

D6 = []
R6 = []
m6 = 6

D7 = [3, 6, 7]
R7 = [5, 15, 10]
m7 = -1

D8 = [3, 6, 7]
R8 = [5, 15]
m8 = 2
    
Billboards(D0, R0, m0)
Billboards(D1, R1, m1)
Billboards(D2, R2, m2)
Billboards(D3, R3, m3)
Billboards(D4, R4, m4)
Billboards(D5, R5, m5)
Billboards(D6, R6, m6)
Billboards(D7, R7, m7)
Billboards(D8, R8, m8)