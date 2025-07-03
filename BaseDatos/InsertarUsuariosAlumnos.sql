select * from Usuario where usuNombreIngreso = '59226151'

insert into Usuario(usuApellido, usuNombreIngreso, usuClave, usuActivo, perid, insId)
select A.aluNombre, A.aluDoc, 'TztPTyWPBGvmzJuihUE5maAUn/u1qM+zrqz2yyggh+lDU2si/EfTBRS8DoSiGsG+bjk81u8uiEuT9E8NhCgFXQ==', 1, 16, 121
 from InscripcionCursado I
join Alumno A on A.aluid = I.aluid 
join Curso C on C.curid = I.curid 
where icuAnoCursado = 2025 and 
not exists (select * from Usuario where usuNombreIngreso = A.aluDoc)

