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

    int x = 0, K = 0, L = 0, M = 0;
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
                if (fastPow(3, K) * fastPow(5, L) * fastPow(7, M) == x) {  std::cout << x << ", ";  }
            }
        }
    }
    return 0;
}
