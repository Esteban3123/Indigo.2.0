#language: es
Característica: SaveAndConfirmAccountigDocument
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Comprobante Contable
#	Dado Genero el comprobante Contable
#	Y Yo guardo y confirmo el comprobante Contable
#	Entonces Este es almacenado y Confirmado
	
Esquema del escenario: Guardar y Confirmar Comprobante Contable
	Dado Genero el comprobante Contable 
	Y Yo guardo y confirmo el comprobante Contable <Registro_No>
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
