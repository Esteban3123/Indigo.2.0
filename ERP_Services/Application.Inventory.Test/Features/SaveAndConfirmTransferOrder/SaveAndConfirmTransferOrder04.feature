#language: es
Característica: SaveAndConfirmTransferOrder04
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar orden de traslado
#	Dado Genero la orden de traslado
#	Y Luego guardo y confirmo la orden de traslado
#	Cuando Yo confirmo el comprobante de entrada
#	Entonces Esta es almacenada y Confirmada
	
Esquema del escenario: Guardar y Confirmar orden de traslado
	Dado Genero la orden de traslado
	Y Luego guardo y confirmo la orden de traslado <Sequence_id>
	Entonces Esta es almacenada y Confirmada

	Ejemplos: 
	| Sequence_id |
	| 28 |
