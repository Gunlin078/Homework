#include <iostream>
#include <string>
//3k*5l*7m = x
long long fastPow(long long base, int exp) {
    long long res = 1;
    while (exp > 0) {
        if (exp & 1) res *= base;  

        if (exp == 1) break;

        base *= base;            
        exp >>= 1;              
    }
    return res;
}
int main() {
    setlocale(LC_ALL, "Russian");

    int x, K, L, M;
    int count = 0;
    std::cin >> x;
    while (true) {
        K++;
        if (fastPow(3, K) > x) break;

        while (true) {
            L++;
            if (fastPow(5, L) >= x) break;

            while (true) {
                M++;
                if (fastPow(7, M) >= x) break;
                if (1) {                                    }////
            }
        }
    }
    return 0;
}
