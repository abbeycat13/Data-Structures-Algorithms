#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Mon Mar 16 12:23:50 2020

COIS 4050H: Assignment #2 (Part A), Question 2
@author: S. Chapados

ASTEROIDS PROGRAM FILE
"""

"""
    FUNCTION: Asteroids
    
    Solves Asteroids Redux game - returns max number of asteroids that can
    be destroyed and when to activate the ionic pulse.
    
    Recurrence Relation: 
        M[i] = max(min(F[i], A[i]), M[i-j] + min(F[j], A[i]))  1 <= j < i
    Initial Condition: 
        M[0] = 0, M[1] = min(F[1], A[1])
    
    Time Complexity: O(n^2)
"""
def Asteroids(A, F):
    print("\n\nA = " + str(A[1:]))
    print("F = " + str(F[1:]))
    
    if len(A) < 2 or len(A) != len(F): # make sure program doesn't crash
        print("\nNo game to be played")
        return
    
    """  TABLE BUILDING: O(n^2)  """
    M = [0] # M[i] = Max Number of Asteroids Destroyed after i Seconds
    S = [[0]] # Solution Set (when to activate ionic pulse)
    
    for i in range(1, len(A)):
        M.append(min(F[i], A[i])) # asteroids destroyed if activate at i
        S.append([i]) # add i to solution set
        # check if there's a better solution
        for j in range(1, i):
            if M[i-j] + min(F[j], A[i]) > M[i]:
                M[i] = M[i-j] + min(F[j], A[i])
                S[i] = S[i-j] + [j]
    
    """  OPTIMAL SOLUTION: O(n)  """
    for k in range(1, len(A)):
        print("\nSOLUTION FOR " + str(k) + " SECONDS:" + 
        "\nAsteroids Destroyed: " + str(M[k]) +
        "\nActivate at: " + str(S[k]))


"""  TEST CASES  """
A0 = [0, 4, 5, 1, 7] # Number of Asteroids that Appear at i Seconds
F0 = [0, 2, 3, 6, 8] # Asteroids Destroyed after charging for i Seconds

A1 = [0, 6, 7, 3]
F1 = [0, 2]

A2 = [0]
F2 = [0]

A3 = [0, 1, 2, 3, 4, 5, 6, 7]
F3 = [0, 7, 6, 5, 4, 3, 2, 1]

A4 = [0, 1, 4, 6, 3, 9]
F4 = [0, 1, 1, 1, 1, 12]

A5 = [0, 4]
F5 = [0, 3]

Asteroids(A0, F0)
Asteroids(A1, F1)
Asteroids(A2, F2)
Asteroids(A3, F3)
Asteroids(A4, F4)
Asteroids(A5, F5)