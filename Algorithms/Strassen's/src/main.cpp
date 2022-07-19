#include <iostream>
#include <cstddef>
#include "matrix.hpp"

int main(void)
{
    try
    {
        Matrix A(4, false);
        Matrix B(4, false);
        std::cout << "Matrix A:\n";
        A.print();
        std::cout << "Matrix B:\n";
        B.print();
        std::cout << "A x B:\n";
        (A * B).print();
    }
    catch (const std::exception &e)
    {
        std::cerr << e.what() << '\n';
    }

    return EXIT_SUCCESS;
}
