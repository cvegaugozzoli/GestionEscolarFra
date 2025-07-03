select * from Alumno where aluDoc = '56518188'
select * from InscripcionCursado where icuAnoCursado = 2024 and aluid = 3114
select * from InscripcionConcepto where icuid = 28353 or icuid = 38405

update InscripcionConcepto set icuid = 27224 where icoId = 287546

delete from InscripcionCursado where icuid = 35031

select * from (
select insid, aluid, carid, plaid, curid, camid, icuAnoCursado, icuEstado, icuActivo from InscripcionCursado where icuAnoCursado = 2024
group by insid, aluid, carid, plaid, curid, camid, icuAnoCursado, icuEstado, icuActivo
having count(*)>1
) as Q
join InscripcionCursado I on Q.aluId = I.aluId and Q.camId = I.camId and Q.carId = I.carId and Q.curId = I.curId and Q.icuActivo = I.icuActivo 
and Q.icuAnoCursado = I.icuAnoCursado and Q.icuEstado = I.icuEstado and Q.plaId = I.plaId
where not exists (select * from InscripcionConcepto where InscripcionConcepto.icuId = I.icuid)
order by Q.aluid

select * from (
select insid, aluid, carid, plaid, curid, camid, icuAnoCursado, icuEstado, icuActivo from InscripcionCursado where icuAnoCursado = 2024
group by insid, aluid, carid, plaid, curid, camid, icuAnoCursado, icuEstado, icuActivo
having count(*)>1
) as Q
where not exists (select * from InscripcionConcepto where InscripcionConcepto.icuId = Q.icuid)