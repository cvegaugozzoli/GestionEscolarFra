select * from Usuario where usuNombreIngreso = '57443035'
-- 'EpWywJII07XLxEoZAZNWEiBMkTC1nV272aPiyrRNF6XjZdQNH92b5Qd5rNKq+3BEMUQwipYxiulvgHFalu/Peg=='

select * from InscripcionCursado ICU1
where icuAnoCursado = 2024 and not exists(Select * from InscripcionCursado ICU2 where ICU1.icuid = ICU2.icuid and ICU2.icuAnoCursado <= 2023)

select max(icuid) as icuid from InscripcionCursado where icuAnoCursado = 2023  -- 27185

select distinct I.aluid from InscripcionCursado I
join Alumno A on A.aluid = I.aluid
where icuid > 27185  -- 27185


insert into Usuario(insid, usuApellido, usuNombreIngreso, usuClave, usuCambiarClave, perId, usuActivo) -- Clave "Alumnos2024"
Select 121, aluNombre, aluDoc, 'TztPTyWPBGvmzJuihUE5maAUn/u1qM+zrqz2yyggh+lDU2si/EfTBRS8DoSiGsG+bjk81u8uiEuT9E8NhCgFXQ==',  0, 16, 1 
from (
select distinct I.aluid from InscripcionCursado I
where icuid > 27185  -- 27185
) as Q
join Alumno A on A.aluid = Q.aluid
where A.aluActivo = 1 


Update Usuario set usuClave = 'TztPTyWPBGvmzJuihUE5maAUn/u1qM+zrqz2yyggh+lDU2si/EfTBRS8DoSiGsG+bjk81u8uiEuT9E8NhCgFXQ==' where usuid >=8

select * from Usuario






