'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Enumeracion utilizada para determinar la duracion del objeto en cache
''' </summary>
Public Enum Length As Integer

    ''' <summary>
    ''' Corta duracion
    ''' </summary>
    ShortLength = 0

    ''' <summary>
    ''' Mediana Duracion
    ''' </summary>
    MediumLength = 1

    ''' <summary>
    ''' Larga duracion
    ''' </summary>
    LongLength = 2

End Enum

''' <summary>
''' 	
''' </summary>
Public Enum EModule As Integer

    ''' <summary>
    ''' Modulo de Seguridad
    ''' </summary>
    Seguridad = 0

    ''' <summary>
    ''' Modulo de Contratos
    ''' </summary>
    Contratos = 1

    ''' <summary>
    ''' Modulo de Admisiones
    ''' </summary>
    Admisiones = 2

    ''' <summary>
    ''' Modulo de Hospitalizacion
    ''' </summary>
    Hospitalizacion = 3

    ''' <summary>
    ''' Modulo de Historias Clinicas
    ''' </summary>
    HistoriasClinicas = 4

    ''' <summary>
    ''' Modulo de Inventario
    ''' </summary>
    Inventarios = 5

    ''' <summary>
    ''' Modulo de Cartera
    ''' </summary>
    Cartera = 6

    ''' <summary>
    ''' Modulo de Citas Medicas
    ''' </summary>
    CitasMedicas = 7

    ''' <summary>
    ''' Modulo de Contabilidad
    ''' </summary>
    Contabilidad = 8

    ''' <summary>
    ''' Modulo de Cuentas por Pagar
    ''' </summary>
    CuentasPorPagar = 9

    ''' <summary>
    ''' Modulo de Facturacion
    ''' </summary>
    Facturacion = 10

    ''' <summary>
    ''' Modulo de Programacion de Cirugia
    ''' </summary>
    ProgramacionCirugia = 11

    ''' <summary>
    ''' Modulo de Business Intelligence
    ''' </summary>
    BusinessIntelligence = 12

End Enum