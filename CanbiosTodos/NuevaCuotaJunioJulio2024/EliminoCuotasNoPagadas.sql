use GestionEscolarFra
select * from InscripcionCursado ICU
join InscripcionConcepto ICO on ICU.icuId = ICO.icuId
join ComprobantesDetalle CE on Ce.icoId = ICO.icoid
where icuAnoCursado = 2024  -- 4599

select * from InscripcionCursado ICU
join InscripcionConcepto ICO on ICU.icuId = ICO.icuId
where icuAnoCursado = 2024 and ICO.icoNroCuota >=4 
and not exists (select * from ComprobantesDetalle CE where Ce.icoId = ICO.icoid)  -- 6373

-- Eliminamos 
Delete ICO 
from InscripcionConcepto ICO
join InscripcionCursado ICU on ICU.icuId = ICO.icuId
where icuAnoCursado = 2024 and ICO.icoNroCuota >=4 
and not exists (select * from ComprobantesDetalle CE where Ce.icoId = ICO.icoid)  -- 6373


select * from InscripcionConcepto 



select ICO.icuid, icoid into ppp 
from InscripcionCursado ICU
join InscripcionConcepto ICO on ICU.icuId = ICO.icuId
where icuAnoCursado = 2024 and ICO.icoNroCuota =4  
and exists (select * from ComprobantesDetalle CE where Ce.icoId = ICO.icoid) -- 42



delete ICO
from InscripcionConcepto ICO
join ppp on ppp.icuId = ICO.icuid 
where icoNroCuota = 1 and (conid >=216 and conid <=218)

select * from InscripcionConcepto ICO
join ppp on ppp.icuId = ICO.icuid 
join InscripcionCursado ICU on ICU.icuId = ppp.icuid 
join Alumno A on A.aluid = ICU.aluid 
where icoNroCuota = 1 and (conid >=216 and conid <=218)
