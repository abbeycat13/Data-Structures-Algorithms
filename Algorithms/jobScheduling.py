#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Wed Mar 25 03:49:44 2020

COIS 4050H: Assignment #2 (Part B), Question 1
@author: S. Chapados

JOB SCHEDULING PROGRAM FILE
"""

"""   CLASS: Job                                                           """
""" ---------------------------------------------------------------------- """
class Job:
    def __init__(self, s, p, jID):
        self.s = s # execution time on supercomputer
        self.p = p # execution time on personal computer
        self.t = s + p # total execution time
        self.jobID = jID # unique identifyer
        
    def __lt__(self, other):
        if self.p < other.p:
            return True
        return False
    
    def __str__(self):
        return str(self.jobID)
""" -------------------------------------------------------- END JOB CLASS """

"""
    FUNCTION: Schedule
    
    Schedules computer jobs to minimize overall completion time.
    
    Parameters:
        J - list of Jobs to be scheduled
    
    Time Complexity: O(n) + O(nlogn) (sort function)
"""
def Schedule(J):
    if len(J) < 1: # don't want program to crash on empty list
        print("\n\nNo jobs to schedule.")
        return
    
    # VARIABLES:
    S = list() # all supercomputer times
    P = list() # all PC times
    time = list() # completion time for each job
    jobs = "" # string to contain final solution
    
    for j in J:
        S.append(j.s)
        P.append(j.p)
    print("\n\nS = " + str(S) +
          "\nP = " + str(P))
    
    # greedy choice - sort jobs by PC time descending
    J.sort(reverse = True)
    
    # calculate time to completion
    time.append(J[0].t) # total time for job 0
    time.append(J[0].s) # start time for job 1
    for i in range(1, len(J)): 
        if i < len(J) - 1:
            time.append(time[i] + J[i].s) # next start time
        time[i] += J[i].t # total time for job i
        
    # print solution
    for j in J:
        jobs += str(j.jobID) + " "
    print("\nSOLUTION: " + jobs + "\nTotal Time: " + str(max(time)))


"""  TEST CASES  """
J0 = [Job(2, 6, 1), Job(3, 5, 2), Job(5, 1, 3), Job(4, 9, 4)]
J1 = [Job(2, 8, 1), Job(4, 6, 2), Job(7, 3, 3)]
J2 = [Job(1, 12, 1), Job(20, 6, 2), Job(10, 8, 3)]
J3 = [Job(3, 5, 1), Job(6, 5, 2), Job(5, 5, 3)]
J4 = list()
J5 = [Job(2, 6, 1)]

Schedule(J0)
Schedule(J1)
Schedule(J2)
Schedule(J3)
Schedule(J4)
Schedule(J5)
