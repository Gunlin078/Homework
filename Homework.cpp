#include <algorithm>
#include <iostream>
#include <vector>

#define ULLONG unsigned long long

int main() {
    setlocale(LC_ALL, "Russian");

    int x = 0;
    std::vector<ULLONG> results;
    std::cin >> x;


    ULLONG p3 = 1;
    while (p3<=x) {
        ULLONG p5 = 1;
        while (p3*p5 <= x) {
            ULLONG p7 = 1;
            while (p3 * p5 * p7 <= x) {
                results.push_back(p3 * p5 * p7);
                p7 *= 7;
            }
            p5 *= 5;
        }
        p3 *= 3;
    }
    std::sort(results.begin(), results.end());

    for (ULLONG i : results) {
        std::cout << i << ", ";
    }
    
    return 0;
}
