#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Wed Mar 25 22:00:17 2020

COIS 4050H: Assignment #2 (Part B), Question 3
@author: S. Chapados

SECURITY MONITORING PROGRAM FILE
"""

"""   CLASS: Activity                                                      """
""" ---------------------------------------------------------------------- """
class Activity:
    def __init__(self, s, f):
        self.s = s # start time
        if f > s:
            self.f = f # finish time
        else:
            self.f = s + 1
        self.covered = False
    
    def __lt__(self, other):
        if self.f < other.f:
            return True
        return False
    
    def __str__(self): # print format: xx:xx - xx:xx
        return ("[" + str(self.s)[:-2] + ":" + str(self.s)[-2:] + " - " + 
        str(self.f)[:-2] + ":" + str(self.f)[-2:] + "]")
""" --------------------------------------------------- END ACTIVITY CLASS """           

"""
    FUNCTION: Monitor
    
    Determines minimum number of m-minute (m > 0) time blocks needed for 
    security guard to monitor all activities.
    
    Parameters:
        A - list of Activities to be monitored
    
    Time Complexity: O(n) + O(nlogn) (sort)
"""
def Monitor(A):
    if len(A) < 1: # make sure program doesn't crash on empty list
        print("\n\nThere are no activities to monitor.")
        return
    
    A.sort() # sort list by earliest finish time
    print("\n\nACTIVITIES:")
    for i in range(len(A)):
        print(str(i+1) + ": " + str(A[i]))

    count = 0 # number of times guard needs to monitor
    times = list() # when to monitor
    i = 0
    while i < len(A): # loop until all activities are covered
        f = A[i].f # next finish time
        A[i].covered = True # cover activity
        count += 1
        times.append(str(f)[:-2]+":"+str(f)[-2:])
        # cover all overlapping activities
        j = i + 1
        while j < len(A):
            if A[j].s > f: # end loop if start time is > current finish
                break      # time as no more activities will overlap
            A[j].covered = True
            j += 1
        i = j # everything up to j should now be covered
      
    print("\nSOLUTION:\nMonitor at: " + str(times) + "\nCount: " + str(count))
    
       
"""  TEST CASES  """
A0 = [Activity(1200, 1300), Activity(1300, 1800), Activity(1400, 1630), 
       Activity(1430, 1500), Activity(1600, 1700), Activity(1900, 2000), 
       Activity(1800, 2000)]
A1 = [Activity(800, 1000), Activity(1030, 1100), Activity(1400, 1630), 
       Activity(1900, 2000)]
A2 = [Activity(1300, 1800), Activity(1400, 1630), Activity(1430, 1500)]
A3 = [Activity(1200, 1300), Activity(1300, 1800)]
A4 = []
A5 = [Activity(1900, 2000)]

Monitor(A0)
Monitor(A1)
Monitor(A2)
Monitor(A3)
Monitor(A4)
Monitor(A5)
