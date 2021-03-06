        1.9. Дамба (9)
В рыбохозяйстве "Рыбнастол" принято решение о разведении карасей и щук.
К сожалению, эти рыбы не могут быть вместе в одном водоеме, так как щуки предпочитают питаться исключительно карасями.
Планируется каждое из k озер разделить дамбой на две не сообщающиеся друг с другом части. 
На карте каждое i-е озеро представлено в определенном масштабе прямоугольным  участком  mi × ni единиц,
разбитым на единичные квадраты. Отдельный квадрат целиком занят водой или сушей. 
Множество водных квадратов является связным. Это означает,
что из любого водного квадрата озера можно попасть в любой другой водный квадрат,
последовательно переходя по водным квадратам через их общую сторону. 
Такие последовательности связанных квадратов будем называть путями.
Дамба имеет вид непрерывной ломаной, проходящей по сторонам квадратов.
Два водных квадрата, общая сторона которых принадлежит ломаной, становятся не сообщающимися напрямую друг с другом. 
Требуется для каждого озера определить минимальную по количеству сторон длину дамбы, разделяющей озеро на две не сообщающиеся между собой связные части. 
Чтобы обеспечить доступ рыбаков к воде, каждая из двух полученных частей озера должна сообщаться с берегом. 
Иными словами, каждая часть должна содержать водный квадрат, который либо сам находится на границе исходного прямоугольного участка,
либо имеет общую сторону с квадратом суши, связанным с границей путем из квадратов суши. 
Ввод. В первой строке содержится число k (1 ≤ k ≤ 10). В следующих строках следуют k блоков данных. 
Каждый блок описывает одно озеро. В первой строке блока содержатся числа mi и ni, разделенные пробелом.
В следующих mi строках находится матрица, представляющая озеро, по ni символов в строке. Символ '.' обозначает квадрат суши, 
а символ '#' – квадрат воды. Гарантируется наличие не менее двух водных квадратов. Общая площадь озер s = m1 × n1 + m2 × n2 + … + mk × nk не должна превосходить 106.
Выходные данные. В единственной строке должны выводиться через пробел k значений, 
определяющих минимальные длины дамб. В результате каждое озеро должно быть разделено на две части так, 
что водные клетки из разных частей не могут иметь общей стороны, не принадлежащей дамбе. 
Тем не менее, касание этих клеток углами допускается. Каждая часть должна быть связана с берегом так, как это определялось выше.
