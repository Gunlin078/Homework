#include <iostream>
#include <string>
//3k*5l*7m = x
long long fast_pow(long long base, int exp) {
    long long res = 1;
    while (exp > 0) {
        if (exp & 1) res *= base;  // Если степень нечетная (проверка младшего бита)
        base *= base;              // Возводим основание в квадрат
        exp >>= 1;                 // Делим степень на 2 (сдвиг бита вправо)
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
        if (3**K > x) break;

        while (true) {
            L++;
            if (L * 5 >= x) break;

            while (true) {
                M++;
                if (M * 7 >= x) break;
                if (K * 3)
            }
        }
    }
    return 0;
}
