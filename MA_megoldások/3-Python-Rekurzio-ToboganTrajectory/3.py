def fa(ls, x0, y0, dx, dy, w, h):
  ujx = (x0+dx)%w
  ujy = y0+dy
  if h <= ujy:
    return 0
  return fa(ls, ujx, ujy, dx, dy, w, h) + (1 if ls[ujy][ujx] == "#" else 0)

feladatok = [(1,1), (3,1), (5,1), (7,1), (1,2)]

p = 1
for s in feladatok:
  print(f'feladat: {s}')
  reszeredmeny = fa(m,0,0,s[0],s[1],len(m[0]),len(m))
  print(reszeredmeny)
  p*=reszeredmeny

print(f'szorzat: {p}')