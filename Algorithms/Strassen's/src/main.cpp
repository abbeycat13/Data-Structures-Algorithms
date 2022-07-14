#include <iostream>
#include <cstddef>
#include "matrix.hpp"

int main(void)
{
    try
    {
        Matrix a(2, false);
        Matrix b(2, false);
        a.print();
        b.print();
        Matrix c = a * b;
        c.print();
    }
    catch (const std::exception &e)
    {
        std::cerr << e.what() << '\n';
    }

    return EXIT_SUCCESS;
}
