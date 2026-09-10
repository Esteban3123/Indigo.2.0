'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountManagementRespository
' Author           : Felix Camilo Salazar Roldan
' Created          : 2024-12-12
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
#End Region

''' <summary>
''' Servicio de acceso a datos para el módulo de Gestión de Cuentas (Account Management).
''' Proporciona métodos para consultar vistas y entidades XPO relacionadas con la gestión de folios, áreas y traslados.
''' </summary>
Public Class AccountManagementServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"
    
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <param name="criteria">Cadena de criterio para filtrar (sintaxis CriteriaOperator)</param>
    ''' <returns>Lista tipo T</returns>
    ''' <remarks>
    ''' Este método es el estándar usado por TODOS los reportes DevExpress XtraReports.
    ''' Devuelve List(Of T) que es compatible con BindingSource.
    ''' </remarks>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    ''' <summary>
    ''' Obtiene todas las áreas de gestión disponibles en el sistema.
    ''' </summary>
    ''' <returns>Lista de objetos ManagementAreasXpo con todas las áreas de gestión.</returns>
    Public Function ListManagementAreas() As List(Of ManagementAreasXpo)
        Dim session As New IndigoXPOSession(Of ManagementAreasXpo)
        Dim xpQuery = New XPQuery(Of ManagementAreasXpo)(session)
        Return xpQuery.ToList()
    End Function

    ''' <summary>
    ''' Obtiene las áreas de gestión como fuente de datos con retroalimentación instantánea.
    ''' Utilizado para controles DevExpress con carga diferida de datos.
    ''' </summary>
    ''' <returns>XPInstantFeedbackSource configurado con las propiedades Id, Code y Name.</returns>
    Public Function ListManagementAreasXpo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ManagementAreasXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ManagementAreasXpo)),
                                                    "Id;Code;Name", Nothing)
    End Function



    ''' <summary>
    ''' Obtiene todos los motivos de rechazo de traslados de folios.
    ''' Utilizado para controles DevExpress con carga diferida de datos.
    ''' </summary>
    ''' <returns>XPInstantFeedbackSource con los motivos de rechazo disponibles (Id, Code, Name, Status, CodeName).</returns>
    Public Function ListRejectionReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ManagementAreasXpo)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(RejectionReasonXpo)),
                                                          "Id;Code;Name;Status;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Obtiene los traslados de folios asignados a un usuario específico en un centro de atención.
    ''' Filtra los registros de la vista ViewFolioTransferInformation por usuario propietario y centro de atención.
    ''' </summary>
    ''' <param name="userCode">Código del usuario propietario actual de los folios.</param>
    ''' <param name="attentionCenterCode">Código del centro de atención.</param>
    ''' <returns>XPInstantFeedbackSource con los traslados de folios filtrados.</returns>
    Public Function ListTransfersByUserCode(userCode As String, attentionCenterCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewFolioTransferInformationXpo)
        Dim strCriteria = $"CurrentOwnerCode = '{userCode}' AND AttentionCenterCode = '{attentionCenterCode}'"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewFolioTransferInformationXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Obtiene los ingresos pendientes de asignación automática en gestión de cuentas.
    ''' Filtra los registros de la vista ViewAdmissionsPending por centro de atención y tipo de ingreso.
    ''' </summary>
    ''' <param name="attentionCenterCode">Código del centro de atención.</param>
    ''' <param name="typeIncome">Tipo de ingreso del paciente.</param>
    ''' <returns>XPInstantFeedbackSource con los ingresos pendientes filtrados.</returns>
    Public Function ListAdmissionsPending(attentionCenterCode As String, typeIncome As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsPendingXpo)

        Dim typeList As Integer() = typeIncome.Split(","c).Select(Function(s) Integer.Parse(s.Trim())).ToArray()
        Dim inSql As String = String.Join(",", typeList)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"AttentionCenterCode = ? AND TypeIncome In ({inSql})", attentionCenterCode)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewAdmissionsPendingXpo)), Nothing, criteria)
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class