select * from Conceptos order by conid desc

select * from Conceptos order by conid desc
update Conceptos set conMesInicio = 3 where (conid >=216 and conid <=218)
select * from InscripcionConcepto where (conid >=216 and conid <=218)
Update InscripcionConcepto set icoNroCuota = 4 where (conid >=216 and conid <=218)
select * from ConceptosIntereses where (conid >=216 and conid <=218)
Update ConceptosIntereses set coiNroCuota = 4 where (conid >=216 and conid <=218)

Update InscripcionConcepto set icoNroCuota = 4 where (conid >=216 and conid <=218)



Update InscripcionConcepto set icoNroCuota = 10 where icoNroCuota = 6 and (conid >=219 and conid <=221)
Update InscripcionConcepto set icoNroCuota = 9 where icoNroCuota = 5 and (conid >=219 and conid <=221)
Update InscripcionConcepto set icoNroCuota = 8 where icoNroCuota = 4 and (conid >=219 and conid <=221)
Update InscripcionConcepto set icoNroCuota = 7 where icoNroCuota = 3 and (conid >=219 and conid <=221)
Update InscripcionConcepto set icoNroCuota = 6 where icoNroCuota = 2 and (conid >=219 and conid <=221)
Update InscripcionConcepto set icoNroCuota = 5 where icoNroCuota = 1 and (conid >=219 and conid <=221)

select * from Alumno where aluDoc = '50871467'
select * from InscripcionCursado where aluid = 6651
select * from InscripcionConcepto where icuid = 34974