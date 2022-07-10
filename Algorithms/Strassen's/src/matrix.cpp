#include "matrix.hpp"
#include <random>
#include <iostream>

Matrix::Matrix(size_t s)
{
    // create a square matrix of size s
    m_size = s;
    m_data.reserve(s * s);

    // populate the matrix with random ints
    std::random_device rd; // obtains seed for the random number engine
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> distrib(-20, 20);
    for (size_t i = 0; i < s * s; ++i)
    {
        m_data.push_back(distrib(gen));
    }
}

void Matrix::print() const
{
    for (size_t i = 0; i < m_size; ++i)
    {
        for (size_t j = 0; j < m_size; ++j)
            std::cout << at(i, j) << " ";
        std::cout << "\n";
    }
}

int Matrix::at(size_t r, size_t c) const
{
    size_t i = r * m_size + c;
    if (i < m_size * m_size)
        return m_data[i];
    throw std::out_of_range("Matrix::at() : index out of range");
}

Matrix Matrix::multiply(Matrix const &other)
{

    return Matrix(m_size);
}

Matrix Matrix::operator*(Matrix const &other) const
{
    if (m_size != other.m_size)
        throw std::out_of_range("Matrices must be the same size");

    return Matrix(m_size);
}