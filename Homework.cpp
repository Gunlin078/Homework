#include <iostream>
#include <numeric>
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

    int x = 0;
    std::vector<int> results;
    std::cin >> x;

    //std::vector<int> xi(x);
    //std::iota(xi.begin(), xi.end(), 0);


    int p3 = 1;
    while (p3<=x) {
        int p5 = 1;
        while (p3*p5 <= x) {
            int p7 = 1;
            while (p3 * p5 * p7 <= x) {
                results.push_back(p3 * p5 * p7);
                p7 *= 7;
            }
            p5 *= 5;
        }
        p3 *= 3;
    }
    for (int i : results) {
        std::cout << i << ", ";
    }
    
    return 0;
}
