using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicExpermedMT.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auditoria_login",
                columns: table => new
                {
                    id_auditoria_login = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    fecha_login = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    exito = table.Column<bool>(type: "bit", nullable: false),
                    direccion_ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    mensaje = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__auditori__53B2797DB08397A4", x => x.id_auditoria_login);
                });

            migrationBuilder.CreateTable(
                name: "catalogo",
                columns: table => new
                {
                    id_catalogo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_catalogo = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariocreacion_catalogo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fechamodificacion_catalogo = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariomodificacion_catalogo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    descripcion_catalogo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_catalogo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uuid_catalogo = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    estado_catalogo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__catalogo__4B673DCAFB313139", x => x.id_catalogo);
                });

            migrationBuilder.CreateTable(
                name: "diagnostico",
                columns: table => new
                {
                    id_diagnostico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_diagnostico = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_diagnostico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_diagnostico = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uuid_diagnostico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado_diagnostico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__diagnost__1384B745F05B3F12", x => x.id_diagnostico);
                });

            migrationBuilder.CreateTable(
                name: "especialidad",
                columns: table => new
                {
                    id_especialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_especialidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_especialidad = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_especialidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uuid_especialidad = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    estado_especialidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__especial__C1D137632C972C94", x => x.id_especialidad);
                });

            migrationBuilder.CreateTable(
                name: "establecimiento",
                columns: table => new
                {
                    id_establecimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_establecimiento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechamodificacion_establecimiento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    descripcion_establecimiento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    direccion_establecimiento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ciudad_establecimiento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    provincia_establecimiento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    estado_establecimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__establec__AFEAEA2093D0B46B", x => x.id_establecimiento);
                });

            migrationBuilder.CreateTable(
                name: "imagen",
                columns: table => new
                {
                    id_imagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_imagen = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uuid_imagen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado_imagen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__imagen__27CC26890C47734E", x => x.id_imagen);
                });

            migrationBuilder.CreateTable(
                name: "laboratorio",
                columns: table => new
                {
                    id_laboratorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_laboratorio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_laboratorio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_laboratorios = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uuid_laboratorios = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado_laboratorios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__laborato__781B42E2F4B0A54C", x => x.id_laboratorio);
                });

            migrationBuilder.CreateTable(
                name: "medicamentos",
                columns: table => new
                {
                    id_medicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_medicamento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_medicamento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    categoria_medicamento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    distintivo_medicamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    concentracion_medicamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    uuid_medicamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado_medicamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicame__2588C032C3A058E2", x => x.id_medicamento);
                });

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    id_pais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_pais = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    iso_pais = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    codigo_pais = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    gentilicio_pais = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    estado_pais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pais__0941A3A74C5D1C64", x => x.id_pais);
                });

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    id_perfil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_perfil = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion_perfil = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    fechacreacion_perfil = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    estado_perfil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__perfil__1D1C87687E96A9D9", x => x.id_perfil);
                });

            migrationBuilder.CreateTable(
                name: "localidad",
                columns: table => new
                {
                    id_localidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_localidad = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariocreacion_localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fechamodificacion_localidad = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariomodificacion_localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nombre_localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gentilicio_localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    prefijo_localidad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    codigo_localidad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    iso_localidad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    isoad_localidad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    cia_localidad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    estado_localidad = table.Column<int>(type: "int", nullable: false),
                    pais_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__localida__9A5E82AAF641E79A", x => x.id_localidad);
                    table.ForeignKey(
                        name: "FK_localidad_pais",
                        column: x => x.pais_id,
                        principalTable: "pais",
                        principalColumn: "id_pais");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ci_usuario = table.Column<int>(type: "int", nullable: false),
                    nombres_usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    apellidos_usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    telefono_usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email_usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fechacreacion_usuario = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fechamodificacion_usuario = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    direccion_usuario = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    firmadigital_usuario = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    codigoqr_usuario = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    codigo_senecyt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    login_usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    codigo_usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    estado_usuario = table.Column<int>(type: "int", nullable: false),
                    perfil_id = table.Column<int>(type: "int", nullable: true),
                    establecimiento_id = table.Column<int>(type: "int", nullable: true),
                    especialidad_id = table.Column<int>(type: "int", nullable: true),
                    intentos_fallidos = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    clave_usuario = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__4E3E04AD3DFE2140", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuario_especialidad",
                        column: x => x.especialidad_id,
                        principalTable: "especialidad",
                        principalColumn: "id_especialidad");
                    table.ForeignKey(
                        name: "FK_usuario_establecimiento",
                        column: x => x.establecimiento_id,
                        principalTable: "establecimiento",
                        principalColumn: "id_establecimiento");
                    table.ForeignKey(
                        name: "FK_usuario_perfil",
                        column: x => x.perfil_id,
                        principalTable: "perfil",
                        principalColumn: "id_perfil");
                });

            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    id_pacientes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_pacientes = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariocreacion_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fechamodificacion_pacientes = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariomodificacion_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tipodocumento_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    ci_pacientes = table.Column<int>(type: "int", nullable: false),
                    primernombre_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    segundonombre_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    primerapellido_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    segundoapellido_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sexo_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    fechanacimiento_pacientes = table.Column<DateTime>(type: "date", nullable: true),
                    edad_pacientes = table.Column<int>(type: "int", nullable: true),
                    tiposangre_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    donante_pacientes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    estadocivil_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    formacionprofesional_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    telefonofijo_pacientes = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    telefonocelular_pacientes = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nacionalidad_pacientes_pa = table.Column<int>(type: "int", nullable: true),
                    provincia_pacientes_l = table.Column<int>(type: "int", nullable: true),
                    direccion_pacientes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ocupacion_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    empresa_pacientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    segurosalud_pacientes_ca = table.Column<int>(type: "int", nullable: true),
                    estado_pacientes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__paciente__D80336DA83EF9D4A", x => x.id_pacientes);
                    table.ForeignKey(
                        name: "FK_pacientes_estadocivil",
                        column: x => x.estadocivil_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_pacientes_formacionprofesional",
                        column: x => x.formacionprofesional_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_pacientes_nacionalidad",
                        column: x => x.nacionalidad_pacientes_pa,
                        principalTable: "pais",
                        principalColumn: "id_pais");
                    table.ForeignKey(
                        name: "FK_pacientes_provincia",
                        column: x => x.provincia_pacientes_l,
                        principalTable: "localidad",
                        principalColumn: "id_localidad");
                    table.ForeignKey(
                        name: "FK_pacientes_segurosalud",
                        column: x => x.segurosalud_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_pacientes_sexo",
                        column: x => x.sexo_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_pacientes_tipodocumento",
                        column: x => x.tipodocumento_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_pacientes_tiposangre",
                        column: x => x.tiposangre_pacientes_ca,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                });

            migrationBuilder.CreateTable(
                name: "tokens_sesion",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    fecha_expiracion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tokens_s__CA90DA7B264B8294", x => x.token);
                    table.ForeignKey(
                        name: "FK__tokens_se__usuar__4D5F7D71",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "consulta",
                columns: table => new
                {
                    id_consulta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_consulta = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariocreacion_consulta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    historial_consulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secuencial_consulta = table.Column<int>(type: "int", nullable: true),
                    paciente_consulta_p = table.Column<int>(type: "int", nullable: false),
                    motivo_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    enfermedad_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombrepariente_consulta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    signosalarma_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reconofarmacologicas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipopariente_consulta = table.Column<int>(type: "int", nullable: true),
                    telefono_pariente_consulta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    temperatura_consulta = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    frecuenciarespiratoria_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    presionarterialsistolica_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    presionarterialdiastolica_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    pulso_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    peso_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    talla_consulta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    plantratamiento_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacion_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    antecedentespersonales_consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diasincapacidad_consulta = table.Column<int>(type: "int", nullable: true),
                    medico_consulta_d = table.Column<int>(type: "int", nullable: false),
                    especialidad_id = table.Column<int>(type: "int", nullable: false),
                    estado_consulta_c = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__6F53588B443A8E25", x => x.id_consulta);
                    table.ForeignKey(
                        name: "FK_consulta_especialidad",
                        column: x => x.especialidad_id,
                        principalTable: "especialidad",
                        principalColumn: "id_especialidad");
                    table.ForeignKey(
                        name: "FK_consulta_medico",
                        column: x => x.medico_consulta_d,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                    table.ForeignKey(
                        name: "FK_consulta_paciente",
                        column: x => x.paciente_consulta_p,
                        principalTable: "pacientes",
                        principalColumn: "id_pacientes");
                });

            migrationBuilder.CreateTable(
                name: "antecedentes_familiares",
                columns: table => new
                {
                    id_antecedente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardiopatia = table.Column<bool>(type: "bit", nullable: true),
                    obser_cardiopatia = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_cardiopatia = table.Column<int>(type: "int", nullable: true),
                    diabetes = table.Column<bool>(type: "bit", nullable: true),
                    obser_diabetes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_diabetes = table.Column<int>(type: "int", nullable: true),
                    enf_cardiovascular = table.Column<bool>(type: "bit", nullable: true),
                    obser_enf_cardiovascular = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_enfcardiovascular = table.Column<int>(type: "int", nullable: true),
                    hipertension = table.Column<bool>(type: "bit", nullable: true),
                    obser_hipertension = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_hipertension = table.Column<int>(type: "int", nullable: true),
                    cancer = table.Column<bool>(type: "bit", nullable: true),
                    obser_cancer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_cancer = table.Column<int>(type: "int", nullable: true),
                    tuberculosis = table.Column<bool>(type: "bit", nullable: true),
                    obser_tuberculosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_tuberculosis = table.Column<int>(type: "int", nullable: true),
                    enf_mental = table.Column<bool>(type: "bit", nullable: true),
                    obser_enf_mental = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_enfmental = table.Column<int>(type: "int", nullable: true),
                    enf_infecciosa = table.Column<bool>(type: "bit", nullable: true),
                    obser_enf_infecciosa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_enfinfecciosa = table.Column<int>(type: "int", nullable: true),
                    mal_formacion = table.Column<bool>(type: "bit", nullable: true),
                    obser_mal_formacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_malformacion = table.Column<int>(type: "int", nullable: true),
                    otro = table.Column<bool>(type: "bit", nullable: true),
                    obser_otro = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    parentescocatalogo_otro = table.Column<int>(type: "int", nullable: true),
                    consulta_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__antecede__3E01464147F86EE7", x => x.id_antecedente);
                    table.ForeignKey(
                        name: "FK_antecedentes_familiares_consulta",
                        column: x => x.consulta_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_catalogo_cancer",
                        column: x => x.parentescocatalogo_cancer,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_cardiopatia",
                        column: x => x.parentescocatalogo_cardiopatia,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_diabetes",
                        column: x => x.parentescocatalogo_diabetes,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_enfcardiovascular",
                        column: x => x.parentescocatalogo_enfcardiovascular,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_enfinfecciosa",
                        column: x => x.parentescocatalogo_enfinfecciosa,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_enfmental",
                        column: x => x.parentescocatalogo_enfmental,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_hipertension",
                        column: x => x.parentescocatalogo_hipertension,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_malformacion",
                        column: x => x.parentescocatalogo_malformacion,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_otro",
                        column: x => x.parentescocatalogo_otro,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_catalogo_tuberculosis",
                        column: x => x.parentescocatalogo_tuberculosis,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                });

            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    id_cita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_cita = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    usuariocreacion_cita = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fechadelacita_cita = table.Column<DateTime>(type: "date", nullable: true),
                    horadelacita_cita = table.Column<TimeSpan>(type: "time", nullable: true),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    consulta_id = table.Column<int>(type: "int", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado_cita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cita__6AEC3C090A45E308", x => x.id_cita);
                    table.ForeignKey(
                        name: "FK_cita_consulta",
                        column: x => x.consulta_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_cita_paciente",
                        column: x => x.paciente_id,
                        principalTable: "pacientes",
                        principalColumn: "id_pacientes");
                    table.ForeignKey(
                        name: "FK_cita_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "consulta_alergias",
                columns: table => new
                {
                    id_consulta_alergias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_alergia = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    catalogoalergia_id = table.Column<int>(type: "int", nullable: true),
                    consulta_alergias_int = table.Column<int>(type: "int", nullable: false),
                    observacion_alergias = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    secuencial_alergias = table.Column<int>(type: "int", nullable: true),
                    estado_alergias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__04246C792E5BD936", x => x.id_consulta_alergias);
                    table.ForeignKey(
                        name: "FK_catalogo_alergias",
                        column: x => x.catalogoalergia_id,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_consulta_alergias",
                        column: x => x.consulta_alergias_int,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                });

            migrationBuilder.CreateTable(
                name: "consulta_cirugias",
                columns: table => new
                {
                    id_consulta_cirugias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_cirugia = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    catalogocirugia_id = table.Column<int>(type: "int", nullable: true),
                    consulta_cirugias_id = table.Column<int>(type: "int", nullable: false),
                    observacion_cirugia = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    secuencial_cirugias = table.Column<int>(type: "int", nullable: true),
                    estado_cirugias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__4383EA6F5FAF28A8", x => x.id_consulta_cirugias);
                    table.ForeignKey(
                        name: "FK_catalogo_cirugias",
                        column: x => x.catalogocirugia_id,
                        principalTable: "catalogo",
                        principalColumn: "id_catalogo");
                    table.ForeignKey(
                        name: "FK_consulta_cirugias",
                        column: x => x.consulta_cirugias_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                });

            migrationBuilder.CreateTable(
                name: "consulta_diagnostico",
                columns: table => new
                {
                    id_consulta_diagnostico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diagnostico_id = table.Column<int>(type: "int", nullable: false),
                    consulta_diagnostico_id = table.Column<int>(type: "int", nullable: false),
                    observacion_diagnostico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    presuntivo_diagnosticos = table.Column<bool>(type: "bit", nullable: true),
                    definitivo_diagnosticos = table.Column<bool>(type: "bit", nullable: true),
                    secuencial_diagnostico = table.Column<int>(type: "int", nullable: true),
                    estado_diagnostico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__D8B1E6BDF16A3201", x => x.id_consulta_diagnostico);
                    table.ForeignKey(
                        name: "FK_consulta_diagnostico",
                        column: x => x.consulta_diagnostico_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_consulta_diagnostico_diagnostico",
                        column: x => x.diagnostico_id,
                        principalTable: "diagnostico",
                        principalColumn: "id_diagnostico");
                });

            migrationBuilder.CreateTable(
                name: "consulta_imagen",
                columns: table => new
                {
                    id_consulta_imagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen_id = table.Column<int>(type: "int", nullable: false),
                    consulta_imagen_id = table.Column<int>(type: "int", nullable: false),
                    observacion_imagen = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    cantidad_imagen = table.Column<int>(type: "int", nullable: true),
                    secuencial_imagen = table.Column<int>(type: "int", nullable: true),
                    estado_imagen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__43A0875D9C449380", x => x.id_consulta_imagen);
                    table.ForeignKey(
                        name: "FK_consulta_imagen",
                        column: x => x.consulta_imagen_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_consulta_imagen_imagen",
                        column: x => x.imagen_id,
                        principalTable: "imagen",
                        principalColumn: "id_imagen");
                });

            migrationBuilder.CreateTable(
                name: "consulta_laboratorio",
                columns: table => new
                {
                    id_laboratorio_consulta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad_laboratorio = table.Column<int>(type: "int", nullable: true),
                    consulta_laboratorio_id = table.Column<int>(type: "int", nullable: false),
                    observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    catalogo_laboratorio_id = table.Column<int>(type: "int", nullable: false),
                    secuencial_laboratorio = table.Column<int>(type: "int", nullable: true),
                    estado_laboratorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__14E40796F1459D16", x => x.id_laboratorio_consulta);
                    table.ForeignKey(
                        name: "FK_consulta_laboratorio",
                        column: x => x.consulta_laboratorio_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_consulta_laboratorio_catalogo",
                        column: x => x.catalogo_laboratorio_id,
                        principalTable: "laboratorio",
                        principalColumn: "id_laboratorio");
                });

            migrationBuilder.CreateTable(
                name: "consulta_medicamentos",
                columns: table => new
                {
                    id_consulta_medicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechacreacion_medicamento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    medicamento_id = table.Column<int>(type: "int", nullable: false),
                    consulta_medicamentos_id = table.Column<int>(type: "int", nullable: false),
                    dosis_medicamento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    observacion_medicamento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    secuencial_medicamento = table.Column<int>(type: "int", nullable: true),
                    estado_medicamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__12CF73429EC6E7A0", x => x.id_consulta_medicamento);
                    table.ForeignKey(
                        name: "FK_consulta_medicamentos",
                        column: x => x.consulta_medicamentos_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                    table.ForeignKey(
                        name: "FK_medicamento",
                        column: x => x.medicamento_id,
                        principalTable: "medicamentos",
                        principalColumn: "id_medicamento");
                });

            migrationBuilder.CreateTable(
                name: "examen_fisico",
                columns: table => new
                {
                    id_examen_fisico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cabeza = table.Column<bool>(type: "bit", nullable: true),
                    obser_cabeza = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    cuello = table.Column<bool>(type: "bit", nullable: true),
                    obser_cuello = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    torax = table.Column<bool>(type: "bit", nullable: true),
                    obser_torax = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    abdomen = table.Column<bool>(type: "bit", nullable: true),
                    obser_abdomen = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    pelvis = table.Column<bool>(type: "bit", nullable: true),
                    obser_pelvis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    extremidades = table.Column<bool>(type: "bit", nullable: true),
                    obser_extremidades = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    consulta_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__examen_f__B5B777B9C783C962", x => x.id_examen_fisico);
                    table.ForeignKey(
                        name: "FK_examen_fisico_consulta",
                        column: x => x.consulta_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                });

            migrationBuilder.CreateTable(
                name: "organos_sistemas",
                columns: table => new
                {
                    id_organos_sistemas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    org_sentidos = table.Column<bool>(type: "bit", nullable: true),
                    obser_org_sentidos = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    respiratorio = table.Column<bool>(type: "bit", nullable: true),
                    obser_respiratorio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    cardio_vascular = table.Column<bool>(type: "bit", nullable: true),
                    obser_cardio_vascular = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    digestivo = table.Column<bool>(type: "bit", nullable: true),
                    obser_digestivo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    genital = table.Column<bool>(type: "bit", nullable: true),
                    obser_genital = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    urinario = table.Column<bool>(type: "bit", nullable: true),
                    obser_urinario = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    m_esqueletico = table.Column<bool>(type: "bit", nullable: true),
                    obser_m_esqueletico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    endocrino = table.Column<bool>(type: "bit", nullable: true),
                    obser_endocrino = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    linfatico = table.Column<bool>(type: "bit", nullable: true),
                    obser_linfatico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    nervioso = table.Column<bool>(type: "bit", nullable: true),
                    obser_nervioso = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    consulta_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__organos___222F23833360B62C", x => x.id_organos_sistemas);
                    table.ForeignKey(
                        name: "FK_organos_sistemas_consulta",
                        column: x => x.consulta_id,
                        principalTable: "consulta",
                        principalColumn: "id_consulta");
                });

            migrationBuilder.CreateTable(
                name: "facturacion",
                columns: table => new
                {
                    id_facturacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cita_id = table.Column<int>(type: "int", nullable: false),
                    fecha_facturacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    total_factura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    metodo_pago = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    estado_factura = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__facturac__AC4FC894466372E4", x => x.id_facturacion);
                    table.ForeignKey(
                        name: "FK_facturacion_cita",
                        column: x => x.cita_id,
                        principalTable: "cita",
                        principalColumn: "id_cita");
                });

            migrationBuilder.CreateIndex(
                name: "idx_consulta_id_antecedentes",
                table: "antecedentes_familiares",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_cancer",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_cancer");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_cardiopatia",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_cardiopatia");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_diabetes",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_diabetes");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_enfcardiovascular",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_enfcardiovascular");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_enfinfecciosa",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_enfinfecciosa");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_enfmental",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_enfmental");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_hipertension",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_hipertension");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_malformacion",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_malformacion");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_otro",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_otro");

            migrationBuilder.CreateIndex(
                name: "IX_antecedentes_familiares_parentescocatalogo_tuberculosis",
                table: "antecedentes_familiares",
                column: "parentescocatalogo_tuberculosis");

            migrationBuilder.CreateIndex(
                name: "idx_categoria_catalogo",
                table: "catalogo",
                column: "categoria_catalogo");

            migrationBuilder.CreateIndex(
                name: "idx_estado_catalogo",
                table: "catalogo",
                column: "estado_catalogo");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_id_cita",
                table: "cita",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_cita",
                table: "cita",
                column: "estado_cita");

            migrationBuilder.CreateIndex(
                name: "idx_paciente_id_cita",
                table: "cita",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "idx_usuario_id_cita",
                table: "cita",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_consulta_c",
                table: "consulta",
                column: "estado_consulta_c");

            migrationBuilder.CreateIndex(
                name: "idx_medico_consulta_d",
                table: "consulta",
                column: "medico_consulta_d");

            migrationBuilder.CreateIndex(
                name: "idx_paciente_consulta_p",
                table: "consulta",
                column: "paciente_consulta_p");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_especialidad_id",
                table: "consulta",
                column: "especialidad_id");

            migrationBuilder.CreateIndex(
                name: "idx_catalogoalergia_id",
                table: "consulta_alergias",
                column: "catalogoalergia_id");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_alergias_int",
                table: "consulta_alergias",
                column: "consulta_alergias_int");

            migrationBuilder.CreateIndex(
                name: "idx_estado_alergias",
                table: "consulta_alergias",
                column: "estado_alergias");

            migrationBuilder.CreateIndex(
                name: "idx_catalogocirugia_id",
                table: "consulta_cirugias",
                column: "catalogocirugia_id");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_cirugias_id",
                table: "consulta_cirugias",
                column: "consulta_cirugias_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_cirugias",
                table: "consulta_cirugias",
                column: "estado_cirugias");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_diagnostico_id",
                table: "consulta_diagnostico",
                column: "consulta_diagnostico_id");

            migrationBuilder.CreateIndex(
                name: "idx_diagnostico_id",
                table: "consulta_diagnostico",
                column: "diagnostico_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_diagnostico",
                table: "consulta_diagnostico",
                column: "estado_diagnostico");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_imagen_id",
                table: "consulta_imagen",
                column: "consulta_imagen_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_imagen",
                table: "consulta_imagen",
                column: "estado_imagen");

            migrationBuilder.CreateIndex(
                name: "idx_imagen_id",
                table: "consulta_imagen",
                column: "imagen_id");

            migrationBuilder.CreateIndex(
                name: "idx_catalogo_laboratorio_id",
                table: "consulta_laboratorio",
                column: "catalogo_laboratorio_id");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_laboratorio_id",
                table: "consulta_laboratorio",
                column: "consulta_laboratorio_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_laboratorio",
                table: "consulta_laboratorio",
                column: "estado_laboratorio");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_medicamentos_id",
                table: "consulta_medicamentos",
                column: "consulta_medicamentos_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_medicamento",
                table: "consulta_medicamentos",
                column: "estado_medicamento");

            migrationBuilder.CreateIndex(
                name: "idx_medicamento_id",
                table: "consulta_medicamentos",
                column: "medicamento_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_diagnostico1",
                table: "diagnostico",
                column: "estado_diagnostico");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_diagnostico",
                table: "diagnostico",
                column: "nombre_diagnostico");

            migrationBuilder.CreateIndex(
                name: "UQ__diagnost__A5CF086EBD7B0A20",
                table: "diagnostico",
                column: "uuid_diagnostico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_estado_especialidad",
                table: "especialidad",
                column: "estado_especialidad");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_especialidad",
                table: "especialidad",
                column: "nombre_especialidad");

            migrationBuilder.CreateIndex(
                name: "idx_ciudad_establecimiento",
                table: "establecimiento",
                column: "ciudad_establecimiento");

            migrationBuilder.CreateIndex(
                name: "idx_estado_establecimiento",
                table: "establecimiento",
                column: "estado_establecimiento");

            migrationBuilder.CreateIndex(
                name: "idx_consulta_id_examen",
                table: "examen_fisico",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "idx_cita_id_facturacion",
                table: "facturacion",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_imagen1",
                table: "imagen",
                column: "estado_imagen");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_imagen",
                table: "imagen",
                column: "nombre_imagen");

            migrationBuilder.CreateIndex(
                name: "UQ__imagen__448604A0BED75E6D",
                table: "imagen",
                column: "uuid_imagen",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_estado_laboratorios",
                table: "laboratorio",
                column: "estado_laboratorios");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_laboratorio",
                table: "laboratorio",
                column: "nombre_laboratorio");

            migrationBuilder.CreateIndex(
                name: "UQ__laborato__4D3AAC17EEE29786",
                table: "laboratorio",
                column: "uuid_laboratorios",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_estado_localidad",
                table: "localidad",
                column: "estado_localidad");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_localidad",
                table: "localidad",
                column: "nombre_localidad");

            migrationBuilder.CreateIndex(
                name: "IX_localidad_pais_id",
                table: "localidad",
                column: "pais_id");

            migrationBuilder.CreateIndex(
                name: "idx_estado_medicamento1",
                table: "medicamentos",
                column: "estado_medicamento");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_medicamento",
                table: "medicamentos",
                column: "nombre_medicamento");

            migrationBuilder.CreateIndex(
                name: "UQ__medicame__307E4C1D49D0EF60",
                table: "medicamentos",
                column: "uuid_medicamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_consulta_id_organos",
                table: "organos_sistemas",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "idx_ci_pacientes",
                table: "pacientes",
                column: "ci_pacientes");

            migrationBuilder.CreateIndex(
                name: "idx_estado_pacientes",
                table: "pacientes",
                column: "estado_pacientes");

            migrationBuilder.CreateIndex(
                name: "idx_primerapellido_pacientes",
                table: "pacientes",
                column: "primerapellido_pacientes");

            migrationBuilder.CreateIndex(
                name: "idx_primernombre_pacientes",
                table: "pacientes",
                column: "primernombre_pacientes");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_estadocivil_pacientes_ca",
                table: "pacientes",
                column: "estadocivil_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_formacionprofesional_pacientes_ca",
                table: "pacientes",
                column: "formacionprofesional_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_nacionalidad_pacientes_pa",
                table: "pacientes",
                column: "nacionalidad_pacientes_pa");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_provincia_pacientes_l",
                table: "pacientes",
                column: "provincia_pacientes_l");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_segurosalud_pacientes_ca",
                table: "pacientes",
                column: "segurosalud_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_sexo_pacientes_ca",
                table: "pacientes",
                column: "sexo_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_tipodocumento_pacientes_ca",
                table: "pacientes",
                column: "tipodocumento_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_tiposangre_pacientes_ca",
                table: "pacientes",
                column: "tiposangre_pacientes_ca");

            migrationBuilder.CreateIndex(
                name: "UQ__paciente__47B248370877C4FE",
                table: "pacientes",
                column: "ci_pacientes",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_estado_pais",
                table: "pais",
                column: "estado_pais");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_pais",
                table: "pais",
                column: "nombre_pais");

            migrationBuilder.CreateIndex(
                name: "UQ__pais__5515698EF912A078",
                table: "pais",
                column: "iso_pais",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_estado_perfil",
                table: "perfil",
                column: "estado_perfil");

            migrationBuilder.CreateIndex(
                name: "idx_nombre_perfil",
                table: "perfil",
                column: "nombre_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_sesion_usuario_id",
                table: "tokens_sesion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_apellidos_usuario",
                table: "usuario",
                column: "apellidos_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_ci_usuario",
                table: "usuario",
                column: "ci_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_email_usuario",
                table: "usuario",
                column: "email_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_estado_usuario",
                table: "usuario",
                column: "estado_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_nombres_usuario",
                table: "usuario",
                column: "nombres_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_especialidad_id",
                table: "usuario",
                column: "especialidad_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_establecimiento_id",
                table: "usuario",
                column: "establecimiento_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_perfil_id",
                table: "usuario",
                column: "perfil_id");

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__38AEB2A8C61AEF3F",
                table: "usuario",
                column: "ci_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__CD3151FFBB29EE37",
                table: "usuario",
                column: "email_usuario",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "antecedentes_familiares");

            migrationBuilder.DropTable(
                name: "auditoria_login");

            migrationBuilder.DropTable(
                name: "consulta_alergias");

            migrationBuilder.DropTable(
                name: "consulta_cirugias");

            migrationBuilder.DropTable(
                name: "consulta_diagnostico");

            migrationBuilder.DropTable(
                name: "consulta_imagen");

            migrationBuilder.DropTable(
                name: "consulta_laboratorio");

            migrationBuilder.DropTable(
                name: "consulta_medicamentos");

            migrationBuilder.DropTable(
                name: "examen_fisico");

            migrationBuilder.DropTable(
                name: "facturacion");

            migrationBuilder.DropTable(
                name: "organos_sistemas");

            migrationBuilder.DropTable(
                name: "tokens_sesion");

            migrationBuilder.DropTable(
                name: "diagnostico");

            migrationBuilder.DropTable(
                name: "imagen");

            migrationBuilder.DropTable(
                name: "laboratorio");

            migrationBuilder.DropTable(
                name: "medicamentos");

            migrationBuilder.DropTable(
                name: "cita");

            migrationBuilder.DropTable(
                name: "consulta");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropTable(
                name: "especialidad");

            migrationBuilder.DropTable(
                name: "establecimiento");

            migrationBuilder.DropTable(
                name: "perfil");

            migrationBuilder.DropTable(
                name: "catalogo");

            migrationBuilder.DropTable(
                name: "localidad");

            migrationBuilder.DropTable(
                name: "pais");
        }
    }
}
