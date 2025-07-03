select * from Becas

select C.curNombre, A.aluNombre, A.aluCuit, ICU.*, ICO.icoid from InscripcionCursado ICU
join InscripcionConcepto ICO on ICO.icuid = ICU.icuid
join Alumno A on A.aluid = ICU.aluid 
join Curso C on C.curid = Icu.curid 
where becid <> 14 and icuAnoCursado = 2020 -- and ICU.icuid = 9209 -- (icuAnoCursado >= 2020 and icuAnoCursado <= 2022)
and not exists 
(select * from ComprobantesDetalle CD
join ComprobantesCabecera CC on CD.cocId = CC.cocid 
where CD.icoid = ICO.icoid) 

select * from InscripcionConcepto ICO where icuid = 9209
and not exists 
(select * from ComprobantesDetalle CD
join ComprobantesCabecera CC on CD.cocId = CC.cocid 
where CD.icoid = ICO.icoid) 

select * from InscripcionConcepto where conid = 192 and icuid = 9209
Update InscripcionConcepto set icoImporte = '37200' where conid = 192 and icuid = 9209
