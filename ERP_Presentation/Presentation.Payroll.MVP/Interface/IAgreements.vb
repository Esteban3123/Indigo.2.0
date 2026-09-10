'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Rafael Patiño
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IAgreements
    Inherits ICrudBase

#Region "Propiedades"

    ''' <summary>
    ''' propiedad que contiene el estado del convenio
    ''' </summary>
    Property StatusAgreements As Integer
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Asigna la lista de compañias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListCompany As List(Of Company)
    ''' <summary>
    ''' Asigna o obtiene la lista de conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListConcept As List(Of Concept)
    ''' <summary>
    ''' Asigna o obtiene la lista de empleados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    WriteOnly Property ListEmployee As Object
    ''' <summary>
    ''' Propiedad que almacena el valor del convenio para sus respectivos calculos
    ''' </summary>
    ''' <returns></returns>
    Property AgreementValue As Decimal
    ''' <summary>
    ''' Almacena el valor a pagar en cada liquidación
    ''' </summary>
    ''' <returns></returns>
    Property ShareValuePaid As Decimal
    ''' <summary>
    ''' Asigna o obtiene la lista de clases de convenios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListKindsAgreements As List(Of KindsAgreements)
    ''' <summary>
    ''' obtiene o estable las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListAccountReceivableAccountingXPO As XPInstantFeedbackSource


#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Sub AsyncLoader(ByVal State As Boolean)

#End Region

End Interface