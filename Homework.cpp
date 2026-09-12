#include <iostream>
#include <vector>
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

    int x = 0, K = 1, L = 1, M = 1;
    std::vector<int> resuls;
    std::cin >> x;

    int p3 = 1;
    while (p3<=x) {
        int p5 = 1;
        p3 *= 3;
        while (p3*p5 <= x) {
            int p7 = 1;
            p5 *= 5;
            while (p3 * p5 * p7 <= x) {
                p7 *= 7;
            }
        }
    }
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
