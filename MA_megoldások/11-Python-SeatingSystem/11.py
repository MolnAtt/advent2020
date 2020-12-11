class Terem:
  valtozaslista = []
  terkep = []
  w = 0
  h = 0
  szamlalo = 0

  def init(stringterkep):
    Terem.terkep = list(map(list,stringterkep.strip().split('\n')))
    Terem.h = len(Terem.terkep)
    Terem.w = len(Terem.terkep[0])
    print(f'térkép beolvasva, méretei: {Terem.h}x{Terem.w}')
    sor = []
    Terem.szamlalo=0

  def szomszedai(i,j, szomszedsagimod):
    szomszedok = []
    if szomszedsagimod == "közvetlen":
      for x in [-1,0,1]:
        for y in [-1,0,1]:
          if x==0 and y==0:
            continue
          szomszedok.append((i+x,j+y))
      return filter(lambda p: 0<=p[0] and 0<=p[1] and p[0]<Terem.h and p[1]<Terem.w, szomszedok)
    else:
      (x,y) = (i,j)
      for vx in [-1,0,1]:
        for vy in [-1,0,1]:
          if vx==0 and vy==0:
            continue
          (x,y) = (i,j)
          while True:
            (x,y) = (x+vx, y+vy)
            if x in [-1, Terem.h]  or y in [-1, Terem.w]:
              break
            if Terem.terkep[x][y] in "L#":
              szomszedok.append((x,y))
              break
      return szomszedok      

  def uresedik_e(i,j, szomszedok, intolerancia):
    db = 0
    for p in szomszedok:
      if Terem.terkep[p[0]][p[1]] == "#":
        db+=1
        if intolerancia <= db:
          return True
    return False

  def foglalodik_e(i,j, szomszedok):
    return 0 == sum(Terem.terkep[p[0]][p[1]] == "#" for  p in szomszedok)

  def elokeszites(intolerancia, szomszedsagimod):
    Terem.valtozaslista = []
    szomszedok = []
    for i in range(Terem.h):
      for j in range(Terem.w):
        szomszedok = Terem.szomszedai(i,j, szomszedsagimod)
        if Terem.terkep[i][j] == '#' and Terem.uresedik_e(i,j, szomszedok, intolerancia):
          Terem.valtozaslista.append((i,j,'L'))
        if Terem.terkep[i][j] == 'L' and Terem.foglalodik_e(i,j, szomszedok):
          Terem.valtozaslista.append((i,j,'#'))

  def vegrehajtas():
    for tr in Terem.valtozaslista:
      Terem.terkep[tr[0]][tr[1]] = tr[2]

  def ureshelyekszama():
    return sum(b=="#" for b in Terem.stringbe())

  def stringbe():
    r = ""
    for sor in Terem.terkep:
      r+="".join(sor)+"\n"
    return r

  def kiir():
    print(f'------- {Terem.szamlalo} --------')
    print(Terem.stringbe())

  def fejlodes(intolerancia, szomszedsagimod):
    while (True):
      #input()
      Terem.szamlalo += 1
      Terem.kiir()
      Terem.elokeszites(intolerancia, szomszedsagimod)
      if 0 == len(Terem.valtozaslista):
        break
      Terem.vegrehajtas()
    print(Terem.ureshelyekszama())


Terem.init(s2)
Terem.fejlodes(4, "közvetlen")
Terem.ureshelyekszama()


Terem.init(s2)
Terem.fejlodes(5, "látott")
Terem.ureshelyekszama()