'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafal E. Patiño
' Created          : 2013-09-10
'
' Last Modified By : Rafal E. Patiño
' Last Modified On : 2013-09-10
' Description      : Interface del frontal de parametros de interfaz de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports  Domain.Entities

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de parametros interfaces de glosas
''' </summary>
''' <remarks></remarks>
Public Interface IInterfacesParameters
    Inherits IcrudBase

#Region "Propiedades"


    ''' <summary>
    ''' propiedad que contiene el estado del parametro interfaz
    ''' </summary>
    Property StatusInterface As Boolean
    ''' <summary>
    ''' Obtiene o asigna el objeto que encapsula los parametros para la configuracion de interfaces
    ''' </summary>
    ''' <value>Parametros</value>
    ''' <returns>Los parametros</returns>
    Property ObjParametersInterface As GlosasParametersInterface
    ''' <summary>
    ''' Obtiene la lista de cuentas contables para el metodo privado
    ''' </summary>
    ''' <value></value>
    Property AccountsDataSourcePrivate As List(Of  Domain.Entities.SP_AccountsList_Result)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Lista de concepto
    ''' </summary>
    ''' <value></value>
    WriteOnly Property ConceptList As List(Of  Domain.Entities.SP_AccountingConceptList_Result)
    ''' <summary>
    ''' Lista de tipo de documento comprobante contable
    ''' </summary>
    ''' <value></value>
    WriteOnly Property TypeDocumentList As List(Of  Domain.Entities.SP_TypeDocumentList_Result)


#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Sub AsyncLoader(ByVal State As Boolean)

#End Region

End Interface
