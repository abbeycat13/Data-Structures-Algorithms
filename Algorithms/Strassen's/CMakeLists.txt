cmake_minimum_required(VERSION 3.10)

project(Strassen)

set(CMAKE_CXX_STANDARD 20)

add_executable(Strassen 
    src/main.cpp
    src/matrix.hpp
    src/matrix.cpp
)