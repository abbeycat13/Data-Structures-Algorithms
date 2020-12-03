#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Wed Feb  5 13:39:01 2020

COIS 4050H: Assignment #1, Problem 4
@author: S. Chapados

MATRIX CLASS FILE
"""

import random as rng
import numpy as np

class Matrix:
    # constructor
    # creates a square matrix and initializes all values as 0
    def __init__(self, size):
        self.m = list()
        self.rows = size
        for i in range(self.rows):
            t = list()
            for j in range(self.rows):
                t.insert(j, 0)
            self.m.insert(i, t)
    
    # sets each value to random int between 1-9
    def setRandomValues(self):
        for i in range(self.rows):
            for j in range(self.rows):
                self.m[i][j] = rng.randint(1, 9)
            
    # prints the values            
    def printMatrix(self):
        s = ""
        for i in range(self.rows):
            for j in range(self.rows):
                s += str(self.m[i][j]) + " "
            print(s)
            s = ""     
    
    # add together values from 2 lists of same size
    @staticmethod
    def addLists(A, B):
        result = list();
        for i in range(len(A)):
            t = list()
            for j in range(len(A)):
                t.insert(j, A[i][j]+B[i][j])
            result.insert(i, t)
        return result
    
    # subtract values from 2 lists of same size
    @staticmethod
    def subLists(A, B):
        result = list();
        for i in range(len(A)):
            t = list()
            for j in range(len(A)):
                t.insert(j, A[i][j]-B[i][j])
            result.insert(i, t)
        return result
    
    # combine 4 2D lists into one matrix
    @staticmethod
    def combine(A, B, C, D):
        result = list()
        s = len(A)
        for i in range(s):
            t = list()
            for j in range(s):
                t.append(A[i][j])
            for j in range(s):
                t.append(B[i][j])
            result.append(t)
            
        for i in range(s):
            t = list()
            for j in range(s):
                t.append(C[i][j])
            for j in range(s):
                t.append(D[i][j])
            result.append(t)
        return result
    
    # Matrix Multiplication Method
    # Parameters:
    #       A, B - 2D Lists
    #       n - Number of Rows
    # Returns: 2D List
    @staticmethod
    def multiply(A, B, n):
        n = int(n)
        k = int(n/2)
        C = list()
        for i in range(n):
            t = list()
            for j in range(n):
                t.insert(j, 0)
            C.insert(i, t)
        if (n == 1):
            C[0][0] = A[0][0] * B[0][0]
        else:
            A = np.array(A)
            B = np.array(B)
            
            C_11 = Matrix.addLists(Matrix.multiply(A[0:k,0:k], B[0:k,0:k], k), 
             Matrix.multiply(A[0:k,k:n], B[k:n,0:k], k))
            
            C_12 = Matrix.addLists(Matrix.multiply(A[0:k,0:k], B[0:k,k:n], k),
             Matrix.multiply(A[0:k,k:n], B[k:n,k:n], k))
            
            C_21 = Matrix.addLists(Matrix.multiply(A[k:n,0:k], B[0:k,0:k], k),
             Matrix.multiply(A[k:n,k:n], B[k:n,0:k], k))
            
            C_22 = Matrix.addLists(Matrix.multiply(A[k:n,0:k], B[0:k,k:n], k),
             Matrix.multiply(A[k:n,k:n], B[k:n,k:n], k))
            
            C = Matrix.combine(C_11, C_12, C_21, C_22)
        return C
    
    # Strassen's Algorithm for Matrix Multiplication
    # Parameters:
    #       A, B - 2D Lists
    #       n - Number of Rows
    # Returns: 2D List
    @staticmethod
    def strassen(A, B, n):
        n = int(n)
        k = int(n/2)
        C = list()
        for i in range(n):
            t = list()
            for j in range(n):
                t.insert(j, 0)
            C.insert(i, t)
        if (n == 1):
            C[0][0] = A[0][0] * B[0][0]
        else:
            A = np.array(A)
            B = np.array(B)
            
            P1 = Matrix.multiply(A[0:k,0:k], Matrix.subLists(B[0:k,k:n], B[k:n,k:n]), k) # A_11 * S1
            P2 = Matrix.multiply(Matrix.addLists(A[0:k,0:k], A[0:k,k:n]), B[k:n,k:n], k) # S2 * B_22
            P3 = Matrix.multiply(Matrix.addLists(A[k:n,0:k], A[k:n,k:n]), B[0:k,0:k], k) # S3 * B_11
            P4 = Matrix.multiply(A[k:n,k:n], Matrix.subLists(B[k:n,0:k], B[0:k,0:k]), k) # A_22 * S4
            P5 = Matrix.multiply(Matrix.addLists(A[0:k,0:k], A[k:n,k:n]), Matrix.addLists(B[0:k,0:k], B[k:n,k:n]), k) # S5 * S6
            P6 = Matrix.multiply(Matrix.subLists(A[0:k,k:n], A[k:n,k:n]), Matrix.addLists(B[k:n,0:k], B[k:n,k:n]), k) # S7 * S8
            P7 = Matrix.multiply(Matrix.subLists(A[0:k,0:k], A[k:n,0:k]), Matrix.addLists(B[0:k,0:k], B[0:k,k:n]), k) # S9 * S10
            
            C_11 = Matrix.addLists(Matrix.subLists(Matrix.addLists(P5, P4), P2), P6)
            C_12 = Matrix.addLists(P1, P2)
            C_21 = Matrix.addLists(P3, P4)
            C_22 = Matrix.subLists(Matrix.subLists(Matrix.addLists(P5, P1), P3), P7)
            
            C = Matrix.combine(C_11, C_12, C_21, C_22)
        return C
        
