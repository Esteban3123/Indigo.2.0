'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 06-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region
Public Class PPUC

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar los valores de la sesion
    ''' </summary>
    Dim _indigo As SessionValues

    ''' <summary>
    ''' variable utilizada para comunicarse con la interfaz
    ''' </summary>
    Dim _view As IPUC
#End Region

#Region "Constructor"

    ''' <summary>
    ''' Comunica el presentador con la interfaz e inicia la instancia de la singleton
    ''' </summary>
    Public Sub New(ByRef iview As IPUC)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = iview
        _indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Initializes this instance.
    ''' </summary>
    Public Sub initialize()
        Dim modelCityXPO As New MBusqueda
        Me._view.LevelXpo = modelCityXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountLevel)
        _view.ClassAcountingXpo = modelCityXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountClass)
    End Sub

    ''' <summary>
    ''' Obtener si la cuenta es padre
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAccountParent(Id As String) As Boolean
        Dim Filter As String = "IdParent = " & Id & "" 'Si existe como padre
        Dim VarGetAccountParent = XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.GetCollection(Of PUCServiceXpo)(Nothing, Filter).FirstOrDefault()
        If VarGetAccountParent IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountRestriction(mainAccountId As Integer) As DevExpress.Xpo.XPCollection(Of MainAccountRestrictionsXpo)
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListMainAccountRestriction(mainAccountId)
    End Function

#End Region

End Class