PhraseSearch
============
A bit of fun trying to make keyword search over a large set of phrases.

Just indexing terms in aphrase by the first few characheters, stuffing them in a dictionary.

I wanted to do something like the following, but my mind was too tired :(

Basically index by each letter, then a sub index by the next letter and so on.

Apple, Avocado, Ant Eater, Avast, Appear, Apple
split and sorted  

- 	(ant, Ant Eater, term:1, id:3)
- 	(appear, Appear, term:1, id:5)
- 	(apple, Apple, term:1, id:1)
- 	(apple, Apple, term:1, id:6)
- 	(avast, Avast, term:1, id:4)
- 	(avocado, Avocado, term:1, id:2)
- 	(eater, Ant Eater, term:2, id:3)


##Depth 1

#a

- 	(nt, Ant Eater, term:1, id:3)
- 	(ppear, Appear, term:1, id:5)
- 	(pple, Apple, term:1, id:1)
- 	(pple, Apple, term:1, id:6)
- 	(vast, Avast, term:1, id:4)
- 	(vocado, Avocado, term:1, id:2)

e
	(ater, Ant Eater, term:2, id:3)


##Depth 2

#a	
  - #n 
   - (t, Ant Eater, term:1, id:3)

  - #p
   - (pear, Appear, term:1, id:5)
   - (ple, Apple, term:1, id:1)
   - (ple, Apple, term:1, id:6)
		
  - #v
   - (ast, Avast, term:1, id:4)
   - (ocado, Avocado, term:1, id:2)

#e
 - #a
  - (ter, Ant Eater, term:2, id:3)



##Depth 3 ( i can't indent enough...)

#a	
 - #n, t 
   - ('', Ant Eater, term:1, id:3)

 - #p, p 
   - (ear, Appear, term:1, id:5)
   - (le, Apple, term:1, id:1)
   - (le, Apple, term:1, id:6)

 - #v, a
    - (st, Avast, term:1, id:4)

 - #v, o
   -  (cado, Avocado, term:1, id:2)

#e, a, t
 -  (er, Ant Eater, term:2, id:3)
