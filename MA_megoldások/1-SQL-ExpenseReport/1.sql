SELECT TOP 1 F1.szam*F2.szam AS Megoldás
FROM F AS F1, F AS F2
WHERE F1.szam+F2.szam=2020 AND F1.id <> F2.id;

SELECT TOP 1 F1.szam*F2.szam*F3.szam AS Megoldás
FROM F AS F1, F AS F2, F AS F3
WHERE F1.szam+F2.szam+F3.szam=2020 AND F1.id <> F2.id AND F2.id <> F3.id AND F1.id <> F3.id;
