#pragma once
#include <vector>

class Matrix
{
private:
    size_t m_size;
    std::vector<int> m_data;
    Matrix multiply(Matrix const &other);

public:
    Matrix(size_t s);
    void print() const;
    int at(size_t r, size_t c) const;
    Matrix operator*(Matrix const &other) const;
};
