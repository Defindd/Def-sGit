

#include <iostream>
#include <conio.h>
using namespace std;
int main()
{
	int a = 0, temp = 0;
	int tempx = 0, tempy = 0;
	char mas[5][5];
	for (int i = 0; i < 5; i++)
		for (int j = 0; j < 5; j++)
			mas[i][j] = ' ';
	char b = '0', c, d;
	int score=0;
	a = 0;

	while (true)
	{
		a = _getch();
		if (a == 27)break;
		b = (char)a;
		if (b == 's'||b=='r')
		{
			while (true) {
				if (tempx == 0 && tempy == 0)
				{

					if (mas[0][0] == 'a')
						for (int j = 0; j < 5; j++)
						{
							mas[0][j] = 'b';
						}
					else for (int j = 0; j < 5; j++)
					{
						mas[0][j] = 'a';
					}
					if(b=='r')
						for (int j = 1; j < 4; j++)
					{
						mas[0][j] = ' ';
					}
					tempx = 0;
					tempy = 5;
					break;
				}

				if (tempx == 0 && tempy == 5)
				{
					if (mas[1][4] == 'a')
						for (int j = 0; j < 5; j++)
						{
							mas[j][4] = 'b';

						}
					else for (int j = 0; j < 5; j++)
					{
						mas[j][4] = 'a';
					}
					if(b=='r')
						for (int j = 1; j < 4; j++)
					{
						mas[j][4] = ' ';

					}
					tempx = 5;
					tempy = 5;
					break;
				}

				if (tempx == 5 && tempy == 5)
				{
					if (mas[4][3] == 'a')
						for (int j = 4; j >= 0; j--)
						{
							mas[4][j] = 'b';

						}
					else for (int j = 4; j >= 0; j--)
					{

						mas[4][j] = 'a';
					}
					if (b == 'r')
						for (int j = 3; j >= 1; j--)
						{
							mas[4][j] = ' ';

						}
					tempx = 5;
					tempy = 0;
					break;
				}

				if (tempx == 5 && tempy == 0)
				{
					if (mas[3][0] == 'a')
						for (int j = 3; j >= 1; j--)
						{
							mas[j][0] = 'b';

						}
					else for (int j = 4; j >= 0; j--)
					{

						mas[j][0] = 'a';
					}
					if(b=='r')
						for (int j = 4; j >= 0; j--)
						{
							mas[j][0] = ' ';

						}
					tempx = 0;
					tempy = 0;
					break;
				}
			}
			temp++;


			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
					cout << mas[i][j] << ' ';
				cout << endl;
			}
			cout << endl << endl;
			
		}
		if (b == 'c') {
			tempx = 0;
			tempy = 0;
			score += temp;
			temp = 0;
		}
	}
}

