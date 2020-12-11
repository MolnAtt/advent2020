def f(n):
  if n <= 1:
    return 1 
  if n == 2:
    return 2  # 11, 2
  if n == 3:
    return 4  # 111, 12, 21, 3
  return f(n-3)+f(n-2)+f(n-1)
print(f(4))

def feladat_b(s):
  t = list(map(int, s.split(';')))
  t.append(0)
  t.append(max(t)+3)
  t.sort()
  print(t)
  for i in range(len(t)-1):
    t[i]=t[i+1]-t[i]
  t = t[:len(t)-1]
  print(t)
  t = ''.join(list(map(str, t)))
  print(t)
  t =  t.split('3')
  print(t)
  t = list(map(len,t))
  print(t)
  t = list(map(f, t))
  print(t)

  prod = 1
  for szam in t:
    prod *= szam

  print(prod)
  return "szia"

feladat_b(teszt1)
