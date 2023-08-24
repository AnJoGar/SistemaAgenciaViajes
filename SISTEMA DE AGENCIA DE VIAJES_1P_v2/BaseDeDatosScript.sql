Create database SistemaViajes
go

use SistemaViajes
go

create table Usuario
(
id_area varchar(5),
id_empleado varchar(5),
nombre varchar(50),
usuario varchar(10),
contraseña varchar(10)
CONSTRAINT PK_Usuario PRIMARY KEY (id_area, id_empleado)
);

insert into Usuario values('A0001','E0001','Recepcionista','Rec','Rec');
insert into  Usuario values('A0002','E0002','Recepcionista','BrayanM','BrayanM');



create proc sp_logueo_ez
@usuario varchar(10),
@clave varchar(10)
as
select id_area,nombre,usuario,contraseña from Usuario 
where usuario=@usuario and contraseña=@clave
go



------------------------------------------------------------------


create table DestinoTuristico
(
codigo varchar(5),
origen varchar(100),
destino varchar(100),
precio decimal(10,2),
CONSTRAINT PK_codigo PRIMARY KEY (codigo)
);
go

-----
create proc sp_ingreso_destino_turistico
@codigo varchar(5),
@origen varchar(100),
@destino varchar(100),
@precio decimal(10,2),
@accion varchar(50)output
as
if (@accion='1')
begin
	declare @codnuevo varchar(5), @codmax varchar(5)
	set @codmax = (select max(codigo) from destinoturistico)
	set @codmax = isnull(@codmax,'A0000')
	set @codnuevo = 'A'+RIGHT(RIGHT(@codmax,4)+10001,4)
	insert into destinoturistico(codigo,origen,destino,precio)
	values(@codnuevo,@origen,@destino,@precio)
	set @accion='Se generó el código: ' +@codnuevo
end
else if (@accion='2')
begin
	update destinoturistico set origen=@origen, destino=@destino, precio=@precio where codigo=@codigo
	set @accion='Se modificó el código: ' +@codigo
end
else if (@accion='3')
begin
	delete from destinoturistico where codigo=@codigo
	set @accion='Se borró el código: ' +@codigo
end


create proc sp_listar_destino_turistico
as 
select * from DestinoTuristico order by codigo
go



alter proc sp_buscar_DestinoTuristico
@destino varchar(50),
@origen varchar (50),
@tipoBusqueda VARCHAR(50)
as
(select codigo,origen,destino,precio from DestinoTuristico where( @tipoBusqueda = 'Destino' AND destino  like @destino  + '%')
																	OR (@tipoBusqueda = 'Origen' AND origen  like @origen  + '%'))
go



-----------------------------------------------------------

drop table Aerolineas;
create table Aerolineas
(
codigo varchar(5),
nombre varchar(50),
siglas varchar(10),
capacidad INt,
CONSTRAINT PK_codigoA PRIMARY KEY (codigo)
);
go
------
alter proc sp_ingreso_Aerolineas
@codigo varchar(5),
@nombre varchar(50),
@siglas varchar(10),
@capacidad int,
@accion varchar(50)output
as
if (@accion='1')
begin
	declare @codnuevo varchar(5), @codmax varchar(5)
	set @codmax = (select max(codigo) from Aerolineas)
	set @codmax = isnull(@codmax,'A0000')
	set @codnuevo = 'A'+RIGHT(RIGHT(@codmax,4)+10001,4)
	insert into Aerolineas(codigo,nombre,siglas,capacidad)
	values(@codnuevo,@nombre,@siglas,@capacidad)
	set @accion='Se generó el código: ' +@codnuevo
end
else if (@accion='2')
begin
	update Aerolineas set nombre=@nombre, siglas=@siglas, capacidad=@capacidad where codigo=@codigo
	set @accion='Se modificó el código: ' +@codigo
end
else if (@accion='3')
begin
	delete from Aerolineas where codigo=@codigo
	set @accion='Se borró el código: ' +@codigo
end
---------
create proc sp_listar_Aerolineas
as
select * from Aerolineas order by codigo
go

--------
alter proc sp_buscar_Aerolineas
@nombre varchar(50),
@siglas varchar(50),
@tipoBusqueda VARCHAR(50)
as
(select codigo,nombre,siglas,capacidad from Aerolineas where( @tipoBusqueda = 'Nombre'  AND  nombre like @nombre  + '%')
														OR( @tipoBusqueda = 'Siglas'  AND  siglas like @siglas  + '%'))
go


create table Cliente
(
codigo int ,
apellido varchar(50),
nombre varchar(50),
cedula varchar(10),
numero_telefono int,
correo_electronico varchar(50),
direccion varchar(150),
CONSTRAINT PK_codigoCliente PRIMARY KEY (codigo)
);


alter proc sp_buscar_cliente_
@apellido varchar(50),
@nombre varchar (50),
@tipoBusqueda VARCHAR(50)
as
(select codigo,apellido,nombre,cedula, numero_telefono, correo_electronico, direccion from Cliente where( @tipoBusqueda = 'apellido' AND apellido  like @apellido  + '%')
																	OR (@tipoBusqueda = 'nombre' AND nombre  like @nombre  + '%'))
go


---------------------------------------------------------------------

CREATE TABLE Reserva
(
  codigo_reserva INT PRIMARY KEY,
  codigo_destino VARCHAR(5) FOREIGN KEY REFERENCES DestinoTuristico(codigo),
  codigo_aerolinea VARCHAR(5) FOREIGN KEY REFERENCES Aerolineas(codigo),
  codigo_cliente INT FOREIGN KEY REFERENCES Cliente(codigo),
  fecha_reserva DATE,
  fecha_viaje DATE,
  precio_total DECIMAL(10,2),
  CONSTRAINT FK_DestinoTuristico FOREIGN KEY (codigo_destino) REFERENCES DestinoTuristico(codigo),
  CONSTRAINT FK_Aerolineas FOREIGN KEY (codigo_aerolinea) REFERENCES Aerolineas(codigo),
  CONSTRAINT FK_Cliente FOREIGN KEY (codigo_cliente) REFERENCES Cliente(codigo)
);



