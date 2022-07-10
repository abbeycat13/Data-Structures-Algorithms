#pragma once
#include <vector>

class Matrix
{
private:
    size_t m_size;
    std::vector<int> m_data;
    Matrix multiply(Matrix const &other) const;
    size_t get_index(size_t r, size_t c) const;

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
