#language: es
Característica: SaveAndConfirmVoucherTransaction
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Comprobante de Egreso
#	Dado  Genero Maestro y Detalles
#	Y Guardo y confirmo el comprobante de egreso
#	Entonces Este es almacenado y Confirmado
	
Esquema del escenario: Guardar y Confirmar Comprobante de Egreso
	Dado Genero Maestro y Detalles
	Y Guardo y confirmo el comprobante de egreso <Registro_No>
	Entonces Este es almacenado y Confirmado

	Ejemplos: 
	| Registro_No |
	|      1      |
	|      2      |
	|      3      |
	|      4      |
	|      5      |
	|      6      |
	|      7      |
	|      8      |
	|      9      |
	|      10     |
	|      11     |
	|      12     |
	|      13     |
	|      14     |
	|      15     |
	|      16     |
