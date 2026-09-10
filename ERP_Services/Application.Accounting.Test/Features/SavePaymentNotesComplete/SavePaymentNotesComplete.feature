#language: es
Característica: SavePaymentNotesComplete
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Comprobante Contable
#	Dado Genero el comprobante Contable
#	Y Yo guardo y confirmo el comprobante Contable
#	Entonces Este es almacenado y Confirmado
	
Esquema del escenario: Guardar y Confirmar Notas debito y credito
	Dado Genero las Notespayments y las AccountPayables 
	Y Yo guardo y confirmo las Notas debito y credito <Registro_No><id_sequence>
	Entonces Estos son almacenados y Confirmados

	Ejemplos: 
	| Registro_No | id_sequence |
	|      1      |       0     |
	|      2      |       0     |
	|      3      |       0     |
	|      4      |       0     |
	|      5      |       0     |
	|      6      |       0     |
	|      7      |       0     |
	|      8      |       0     |
	|      9      |       0     |
	|      10     |       0     |
	|      11     |       0     |
	|      12     |       0     |
	|      13     |       0     |
	|      14     |       0     |
	|      15     |       0     |
	|      16     |       0     |
