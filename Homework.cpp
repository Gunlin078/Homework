#include <iostream>
#include <intrin.h>
#include <string>

using ULLONG = unsigned long long;
const double K = 1.247;

const unsigned long long PowersOf10[] = {
    1ULL, 10ULL, 100ULL, 1000ULL, 10000ULL, 100000ULL, 1000000ULL, 10000000ULL,
    100000000ULL, 1000000000ULL, 10000000000ULL, 100000000000ULL, 1000000000000ULL,
    10000000000000ULL, 100000000000000ULL, 1000000000000000ULL, 10000000000000000ULL,
    100000000000000000ULL, 1000000000000000000ULL, 10000000000000000000ULL
};

const int GuessTable[] = {
    1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5,
    5, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 9, 9, 9, 10, 10,
    10, 10, 11, 11, 11, 12, 12, 12, 13, 13, 13, 13, 14, 14,
    14, 15, 15, 15, 16, 16, 16, 16, 17, 17, 17, 18, 18, 18,
    19, 19, 19, 19, 20
};

int get_digit_length(unsigned long long n) {
    if (n == 0) return 1;

    unsigned long index;
    _BitScanReverse64(&index, n);
    int bits = index + 1; 

    int length = GuessTable[bits];

    if (n >= PowersOf10[length]) {
        length++;
    }

    return length;
}


int main() {
    setlocale(LC_ALL, "Russian");
    ULLONG number;
    std::cin >> number;

    ULLONG step = number;

    
    return 0;
}
