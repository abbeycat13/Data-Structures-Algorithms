#include "matrix.hpp"
#include <algorithm>
#include <cmath>
#include <iostream>
#include <random>
#include <stdexcept>

// populates the matrix with random values
void Matrix::randomize()
{
    // obtains seed for the random number engine
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution distribution(1, 4);
    std::ranges::for_each (m_data, [&](int& it) {
        it = distribution(gen);
    });
}

Matrix::Matrix(size_t s, bool empty)
{
    // make sure size is a power of 2
    if (std::fmod(std::log2(s), 1) != 0)
        throw std::invalid_argument("Matrix size must be a power of 2\n");

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
    
    if (size_t i = r * m_size + c; i < m_data.size())
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

Matrix Matrix::partition(size_t row_start, size_t col_start) const
{
    size_t const s = m_size / 2; // size of sub_matrix
    Matrix sub_matrix(s);
    for (size_t i = 0; i < s; ++i) {
        for (size_t j = 0; j < s; ++j)
            sub_matrix.at_ref(i, j) = at(i + row_start, j + col_start);
    }
    return sub_matrix;
}

void Matrix::combine(Matrix &R_11, Matrix &R_12, Matrix &R_21, Matrix &R_22)
{
    size_t i, j;
    size_t const k = m_size / 2;

    auto determine_quad = [&] () {
        if (i < k && j < k)
            return R_11.at_ref(i, j);
        if (i < k && j >= k)
            return R_12.at_ref(i, j-k);
        if (i >= k && j < k)
            return R_21.at_ref(i-k, j);
        return R_22.at_ref(i-k, j-k);
    };
    
    for (i = 0; i < m_size; ++i) {
        for (j = 0; j < m_size; ++j) {
            at_ref(i, j) = determine_quad();
        }
    }
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

        Matrix R_11 = partition(0, 0).multiply(other.partition(0, 0)) +
        partition(0, k).multiply(other.partition(k, 0));

        Matrix R_12 = partition(0, 0).multiply(other.partition(0, k)) +
        partition(0, k).multiply(other.partition(k, k));

        Matrix R_21 = partition(k, 0).multiply(other.partition(0, 0)) +
        partition(k, k).multiply(other.partition(k, 0));

        Matrix R_22 = partition(k, 0).multiply(other.partition(0, k)) +
        partition(k, k).multiply(other.partition(k, k));

        result.combine(R_11, R_12, R_21, R_22);
    }

    return result;
}

Matrix Matrix::operator+(Matrix const &other) const
{
    // make sure matrices are the same size (cannot add square matrices of different sizes)
    if (m_size != other.m_size)
        throw std::invalid_argument("Matrices must be the same size");
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
        throw std::invalid_argument("Matrices must be the same size");
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
        throw std::invalid_argument("Matrices must be the same size");

    return multiply(other);
}
