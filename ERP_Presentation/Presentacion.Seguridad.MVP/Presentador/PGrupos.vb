'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Security.Entities
Imports System.Data
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.SecurityRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmGrupos
''' </summary>
Public Class PGrupos

#Region "Variables y Constructor"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IGrupos
    ''' </summary>
    Dim vista As IGrupos
    ''' <summary>
    ''' Variable que serve para instanciar la entidad SeguridadGrupoUsuario
    ''' </summary>
    Dim grupo As Group
    ''' <summary>
    ''' Variable para instanciar la entidad SeguridadGrupoUsuario y hacer el insert
    ''' </summary>
    Dim GrupoUsuario As Group
    ''' <summary>
    ''' Variable Utilizada para utilizar el visor de eventos
    ''' </summary>
    Dim mensaje As New FormBase

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Public Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Constructor que permite la comunicacion con la intefaz Igrupos
    ''' </summary>
    ''' <param name="iview">The vista.</param>
    Public Sub New(ByRef iview As IGrupos)
        If iview Is Nothing Then
            Throw New ArgumentException(obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub

#End Region

#Region "metodos y constructor"
    ''' <summary>
    ''' Este metodo limpia los controles del funcional
    ''' </summary>
    Public Sub Deshacer()
        vista.Deshacer()
        'vista.CodigoDelGrupo = String.Empty
        'vista.NombreDelGrupo = String.Empty
        'vista.ActivarControles = False
        'vista.Instance._doc = Nothing
        vista.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Consultar los grupos por codigo 
    ''' </summary>
    ''' <param name="codigo">El codigo especifico para poder consultar.</param>
    Public Async Sub ConsultarNombresGrupos(ByVal codigo As String)
        Try
            Using modelo As New MGrupos
                vista.AsyncLoader(True)
                grupo = Await modelo.ConsultarNombre(codigo)
                vista.AsyncLoader(False)
                If grupo Is Nothing Then
                    vista.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    Exit Sub
                End If
                If grupo.Id <> 0 Then
                    Select Case Indigo.UserType
                        Case UserType.GlobalAdmin
                        Case UserType.TenantAdmin
                            If grupo.GroupType = 1 Then
                                vista.Mensaje(EeventViewerImages.MensajeError) = "No tiene permiso para editar el rol"
                                Deshacer()
                                Exit Sub
                            End If
                        Case UserType.CompanyAdmin, UserType.StandardUser
                            vista.Mensaje(EeventViewerImages.MensajeError) = "No tiene permiso para editar el rol"
                            Deshacer()
                            Exit Sub
                    End Select

                    vista.PGroupType = grupo.GroupType

                    Dim query As New StringBuilder()

                    query.AppendLine(" SELECT bg.Id, bg.UserGroup, bg.BalancedScorecardId, sc.Name As BalancedScorecardName ")
                    query.AppendLine(" FROM Common.BalancedScorecardByGroup bg												")
                    query.AppendLine(" INNER JOIN Common.BalancedScorecard sc on bg.BalancedScorecardId = sc.Id				")
                    query.AppendLine($" WHERE UserGroup={grupo.Id} ")


                    vista.dtDetails = Await modelo.ConsultarDetales(query.ToString())
                    vista.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    vista.NombreDelGrupo = grupo.Description.Trim
                    vista.ActivarControles = True
                    vista.Instance.GetDocumentIndexed(vista.Tag & "_" & vista.CodigoDelGrupo)

                Else
                    vista.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    vista.NombreDelGrupo = String.Empty
                    vista.ActivarControles = True
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            vista.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End Try
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para Eliminar un Grupo
    ''' </summary>
    Public Async Sub EliminarGrupos()
        Try
            Dim resultado As Boolean
            Using modelo As New MGrupos
                If grupo Is Nothing Then
                    Exit Sub
                End If
                If grupo.Id = 0 Then
                    Exit Sub
                End If
                vista.AsyncLoader(True)
                resultado = Await modelo.EliminarGrupo(grupo)
                vista.AsyncLoader(False)
                If resultado = True Then
                    vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                    Await vista.Instance.DeleteDocumentIndexed()
                    Deshacer()
                Else
                    vista.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(GruposNoEliminado, Grupos)
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            vista.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que Guarda y crea el grupo indicado.
    ''' </summary>
    Public Async Sub GuardarGrupos()
        Try
            Dim resultado As Boolean
            If String.IsNullOrEmpty(vista.CodigoDelGrupo) Or String.IsNullOrEmpty(vista.NombreDelGrupo) Then
                vista.EstablecerFoco("INDTxtNombre") = True
                vista.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
            ElseIf vista.PGroupType Is Nothing Then
                vista.Mensaje(EeventViewerImages.Advertencia) = "Seleccione tipo grupo"
            ElseIf vista.PGroupType IsNot Nothing AndAlso CType(vista.PGroupType, eGroupType) = eGroupType.ByTenant AndAlso (grupo.TenantGroup Is Nothing OrElse grupo.TenantGroup.Count = 0 OrElse
                        Not grupo.TenantGroup.Any(Function(tr) tr.ChangeTracker.State <> ObjectState.Deleted)) Then
                vista.Mensaje(EeventViewerImages.Advertencia) = "Asocie el grupo a un tenant"
            Else
                Using modelo As New MGrupos
                    grupo.Code = vista.CodigoDelGrupo
                    grupo.Description = vista.NombreDelGrupo
                    grupo.GroupType = vista.PGroupType
                    If CType(grupo.GroupType, eRollType) = eRollType.GlobalType AndAlso grupo.TenantGroup IsNot Nothing AndAlso grupo.TenantGroup.Count > 0 AndAlso grupo.TenantGroup.Any(Function(tr) tr.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted) Then
                        grupo.TenantGroup.ToList.ForEach(Sub(tr)
                                                             tr.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                                                             grupo.TenantGroup.Add(tr)
                                                         End Sub)
                    End If
                    vista.AsyncLoader(True)
                    resultado = Await modelo.GrabaGrupo(grupo, vista.dtDetails, vista.ListaEliminados)
                    vista.AsyncLoader(False)
                    vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    vista.Instance.UpdateIndexedDocument(vista.BarraBotones._listDocuments)
                    Deshacer()
                End Using
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            vista.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End Try
    End Sub

    ''' <summary>
    ''' Refresca la informacion de la rejilla tenant
    ''' </summary>
    Public Sub RefreshGridTenant()
        If grupo.TenantGroup Is Nothing Then
            grupo.TenantGroup = New Domain.Base.Entities.TrackableCollection(Of TenantGroup)
        End If
        vista.TenantGroupDataSource = grupo.TenantGroup.Where(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted)
    End Sub

    ''' <summary>
    ''' Agregar un tenant-group a la colleccion
    ''' </summary>
    ''' <param name="_TenantGroup"></param>
    Public Sub AddTenantGroup(_TenantGroup As TenantGroup)
        grupo.TenantGroup.Add(_TenantGroup)
    End Sub

    ''' <summary>
    ''' Quita un tenant-group de la colleccion
    ''' </summary>
    ''' <param name="_TenantGroup"></param>
    Public Sub RemoveTenantGroup(_TenantGroup As TenantGroup)
        grupo.TenantGroup.Remove(_TenantGroup)
    End Sub

    ''' <summary>
    ''' Valida y agrega tenant al grupo
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateAddTenant() As Boolean
        Dim result As Boolean = False
        Dim _Mensaje = "Seleccione: "
        If vista.PGroupType Is Nothing Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tipo grupo")
        End If
        If vista.TenantId Is Nothing OrElse vista.TenantId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tenant")
        End If
        If Not _Mensaje.Equals("Seleccione: ") Then
            vista.Mensaje(EeventViewerImages.MensajeError) = _Mensaje
        Else

            Dim _TenantGroup As TenantGroup = grupo.TenantGroup.Where(Function(tr) tr.TenantId = vista.TenantId.GetValueOrDefault).FirstOrDefault
            If _TenantGroup Is Nothing Then
                _TenantGroup = New TenantGroup() With {.TenantId = vista.TenantId.GetValueOrDefault, .GroupId = grupo.Id}
                Dim _Tenant = TryCast(vista.GetSelectedTenant, TenantXpo)
                If _Tenant IsNot Nothing Then
                    _TenantGroup.TenantName = _Tenant.Name
                End If
                grupo.TenantGroup.Add(_TenantGroup)
            End If
            If _TenantGroup.ChangeTracker.State = ObjectState.Deleted Then
                _TenantGroup.ChangeTracker.State = ObjectState.Modified
            End If
            result = True

        End If
        Return result
    End Function

    ''' <summary>
    ''' Consulta todos los tenants activos
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <param name="pUserType"></param>
    Public Sub GetAllTenant(ByVal UserId As Integer, ByVal pUserType As UserType)
        Using modelo As New MUsuario
            vista.TenantDataSource = modelo.GetAllTenant(UserId, pUserType)
        End Using
    End Sub
#End Region
End Class
