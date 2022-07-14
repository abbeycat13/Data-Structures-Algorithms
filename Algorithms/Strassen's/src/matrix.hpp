#pragma once
#include <vector>

class Matrix
{
private:
    size_t m_size;
    std::vector<int> m_data;
    //static void multiply(Matrix const &A, Matrix const &B, Matrix &result, size_t i, size_t j);
    Matrix multiply(Matrix const &other) const;
    size_t get_index(size_t r, size_t c) const;
    Matrix partition(size_t row_start, size_t col_start) const;
    void combine(Matrix &R_11, Matrix &R_12, Matrix &R_21, Matrix &R_22);

public:
    Matrix(size_t s, bool empty = true);
    void print() const;
    int at(size_t r, size_t c) const;
    int &at_ref(size_t r, size_t c);
    void randomize();
    Matrix operator*(Matrix const &other) const;
    Matrix operator+(Matrix const &other) const;
    Matrix operator-(Matrix const &other) const;
};


