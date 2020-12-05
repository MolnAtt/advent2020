# első rész

def bin(s,i,l,r,k):
  if l==r:
    return l
  return bin(s,i+1,l,(l+r)//2, k) if s[i]==k else bin(s,i+1,(l+r)//2+1,r, k)
def azon(s):
  return bin(s[:7], 0, 0, 127, "F")*8 + bin(s[7:], 0, 0, 7, "L")

foglaltak = list(map(lambda s: azon(s), input.split('\n')))
print(f'ez a legnagyobb azonosítójú szék: {max(foglaltak)}')

# második rész

def enyem(l, b,j):
  if b==j:
    return b+l[0]
  k = (b+j)//2
  return enyem(l, k+1, j) if l[k] == l[0] + k  else enyem(l, b, k)

foglaltak.sort()
print(f'itt ülök: {enyem(foglaltak, 0, len(foglaltak)-1)}')