#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Wed Feb  5 13:09:10 2020

COIS 4050H: Assignment #1, Problem 4
@author: S. Chapados

PROGRAM FILE
"""

import Matrix, stopwatch, math

SIZE = int(math.pow(2, 5))

timer = stopwatch.Stopwatch()

A = Matrix.Matrix(SIZE)
A.setRandomValues() 

B = Matrix.Matrix(SIZE)
B.setRandomValues()

if SIZE < 25:
    print("A:")          
    A.printMatrix() 
    print("\nB:")
    B.printMatrix()

C = Matrix.Matrix(SIZE)

timer.start()
C.m = C.multiply(A.m, B.m, SIZE)
timer.stop()

print("\nA * B (Simple Method):")
if SIZE < 25:
    C.printMatrix()
print("\nTime: " + str(timer))

timer.reset()
timer.start()
C.m = C.strassen(A.m, B.m, SIZE)
timer.stop()

print("\nA * B (Strassen's Method):")
if SIZE < 25:
    C.printMatrix()
print("\nTime: " + str(timer))
timer.reset()
