class Konzol:
  def __init__(self, input, mode):
    self.i = 0
    self.accumulator = 0
    self.l = 0
    if mode == "f":
      with open("input.txt") as f:
        for sor in f:
          self.boot.append(sor)
      self.l = len(boot)
    elif mode == "s":
      self.boot = input.split('\n')
      self.l = len(self.boot)
    else:
      print("fájlból vagy stringből tudok dolgozni, másból nem.")

  def acc(self,a):
    self.accumulator += a
    self.i+=1
  
  def jmp(self,a):
    self.i+=a
  
  def nop(self,a):
    self.i+=1
  
  def run(self):
    merrejartam = set()
    par = []
    while self.i<self.l and self.i not in merrejartam:
      merrejartam.add(self.i)
      par = self.boot[self.i].strip().split(' ')
      argument = int(par[1])
      eval(f'self.{par[0]}({argument})')
    return self.accumulator

teszt = """nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6"""

# 1.rész
gameboy_teszt = Konzol(teszt, "s")
print(gameboy_teszt.run())

gameboy = Konzol("input.txt", "f")
print(gameboy.run())

# 2.rész