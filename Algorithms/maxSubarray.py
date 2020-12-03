#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Fri Mar 13 16:05:05 2020

COIS 4050H: Assignment #2 (Part A), Question 1
@author: S. Chapados

MAXIMUM SUBARRAY PROGRAM FILE
"""

"""
    FUNCTION: maxSubarray
    
    Calculates maximum profit, buy date, and sell date for array of 
    stock prices.
    
    Recurrence Relation: R[j] = R[j-1] + P[j] - P[j-1] 
    Initial Condition: R[0] = 0
    
    Time Complexity: O(n)
"""
def maxSubarray(P):
    print("\n\nARRAY:")
    print(*P) # prints original array on one line
    
    if len(P) < 2: # make sure program doesn't crash on small or empty list
        print("\nSOLUTION:")
        print("No profit to be made.")
        return
    
    """  TABLE BUILDING: O(n)  """
    R = [0] # initial condition for recurrence relation  
    for j in range(1, len(P)):
        R.append(R[j-1]+P[j]-P[j-1]) # add profit for selling on day j
        
    """  OPTIMAL SOLUTION: O(n)  """
    j = R.index(max(R)) # sell date for max profit   
    if j != 0: # if j == 0, no profit can be made
        i = P.index(min(P[0:j])) # buy date for max profit

    print("\nSOLUTION:")
    if j != 0:
        print("Buy on Day "+str(i)+
          "\nSell on Day "+str(j)+
          "\nMax Profit: "+str(P[j]-P[i]))
    else:
        print("No profit to be made.")
        

"""  TEST CASES  """
P0 = [37, 46, 22, 19, 20, 31, 42, 50, 10]
P1 = [78, 23, 56, 13, 91]
P2 = [45, 32, 29, 11]
P3 = [12, 24]
P4 = list()
P5 = [75]
    
maxSubarray(P0)
maxSubarray(P1)
maxSubarray(P2)
maxSubarray(P3)
maxSubarray(P4)
maxSubarray(P5)