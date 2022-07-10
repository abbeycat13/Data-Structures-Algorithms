#include "matrix.hpp"
#include <random>
#include <iostream>
#include <cmath>

// populates the matrix with random values
void Matrix::randomize()
{
    std::random_device rd; // obtains seed for the random number engine
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> const distrib(-20, 20);
    for (size_t i = 0; i < m_data.size(); ++i)
    {
        m_data[i] = distrib(gen);
    }
}

Matrix::Matrix(size_t s, bool empty)
{
    // make sure size is a power of 2
    if (std::fmod(std::log2(s), 1) != 0)
        throw std::exception("Matrix size must be a power of 2\n");

    // create a square matrix of size s
    m_size = s;
    m_data.reserve(s * s);

    for (size_t i = 0; i < s * s; ++i)
    {
        m_data.push_back(0);
    }

    if (!empty)
        randomize();
}

void Matrix::print() const
{
    for (size_t i = 0; i < m_size; ++i)
    {
        for (size_t j = 0; j < m_size; ++j)
            std::cout << at(i, j) << " ";
        std::cout << "\n";
    }
    std::cout << "\n";
}

size_t Matrix::get_index(size_t r, size_t c) const
{
    size_t i = r * m_size + c;
    if (i < m_data.size())
        return i;
    throw std::out_of_range("Matrix::at() : index out of range");
}

int &Matrix::at_ref(size_t r, size_t c)
{
    return m_data[get_index(r, c)];
}

int Matrix::at(size_t r, size_t c) const
{
    return m_data[get_index(r, c)];
}

Matrix Matrix::multiply(Matrix const &other) const
{
    Matrix result(m_size);

    if (m_size == 1)
    {
        result.at_ref(0, 0) = at(0, 0) * other.at(0, 0);
    }
    else
    {
        size_t const k = m_size / 2;
    }

    return result;
}

Matrix Matrix::operator+(Matrix const &other) const
{
    // make sure matrices are the same size (cannot add square matrices of different sizes)
    if (m_size != other.m_size)
        throw std::exception("Matrices must be the same size");
    Matrix result(m_size);

    for (size_t i = 0; i < m_data.size(); ++i) {
        result.m_data[i] = m_data[i] + other.m_data[i];
    }

    return result;
}

Matrix Matrix::operator-(Matrix const &other) const
{
    // make sure matrices are the same size (cannot subtract square matrices of different sizes)
    if (m_size != other.m_size)
        throw std::exception("Matrices must be the same size");
    Matrix result(m_size);

    for (size_t i = 0; i < m_data.size(); ++i) {
        result.m_data[i] = m_data[i] - other.m_data[i];
    }

    return result;
}

Matrix Matrix::operator*(Matrix const &other) const
{
    // make sure matrices are the same size (cannot multiply square matrices of different sizes)
    if (m_size != other.m_size)
        throw std::exception("Matrices must be the same size");

    return multiply(other);
}