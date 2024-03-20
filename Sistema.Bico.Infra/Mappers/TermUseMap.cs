using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Infra.Mappers
{
    public class TermUseMap : IEntityTypeConfiguration<TermUse>
    {
        private const string NOME_TABELA = "TB_TermUse";
        public void Configure(EntityTypeBuilder<TermUse> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");

//            builder.HasData(
//              new TermUse
//              {
//                  Id = Guid.NewGuid(),
//                  Description = @"<h3>TERMO DE USO</h3>

//<p>&nbsp;</p>

//<h4>1. Quais informa&ccedil;&otilde;es est&atilde;o presentes neste documento?</h4>

//<p>Neste Termo de Uso, o usu&aacute;rio do servi&ccedil;o do Di&aacute;rio Oficial da Uni&atilde;o (DOU) disponibilizado pelo aplicativo mobile encontrar&aacute; informa&ccedil;&otilde;es sobre: o funcionamento do servi&ccedil;o e as regras aplic&aacute;veis a ele; o arcabou&ccedil;o legal relacionado &agrave; presta&ccedil;&atilde;o do servi&ccedil;o; as responsabilidades do usu&aacute;rio ao utilizar o servi&ccedil;o; as responsabilidades da Imprensa Nacional ao prover o servi&ccedil;o; informa&ccedil;&otilde;es para contato, caso exista alguma d&uacute;vida ou seja necess&aacute;rio atualizar informa&ccedil;&otilde;es; e o foro respons&aacute;vel por eventuais reclama&ccedil;&otilde;es caso quest&otilde;es deste Termo de Uso tenham sido violadas.</p>

//<h4>2. Aceita&ccedil;&atilde;o do Termo de Uso e Pol&iacute;tica de Privacidade</h4>

//<p>Ao utilizar os servi&ccedil;os, o usu&aacute;rio confirma que leu e compreendeu os Termos e Pol&iacute;ticas aplic&aacute;veis ao servi&ccedil;o Di&aacute;rio Oficial da Uni&atilde;o (DOU) disponibilizado pelo aplicativo mobile e concorda em ficar vinculado a eles.</p>

//<h4>3. Defini&ccedil;&otilde;es</h4>

//<p>Para melhor compreens&atilde;o deste documento, neste Termo de Uso e Pol&iacute;tica de Privacidade, consideram-se:</p>

//<p><strong>Internet:</strong>&nbsp;o sistema constitu&iacute;do do conjunto de protocolos l&oacute;gicos, estruturado em escala mundial para uso p&uacute;blico e irrestrito, com a finalidade de possibilitar a comunica&ccedil;&atilde;o de dados entre terminais por meio de diferentes redes.</p>

//<p><strong>S&iacute;tios e aplicativos:</strong>&nbsp;s&iacute;tios e aplicativos por meio dos quais o usu&aacute;rio acessa os servi&ccedil;os e conte&uacute;dos disponibilizados.</p>

//<p><strong>Usu&aacute;rios (ou &quot;Usu&aacute;rio&quot;, quando individualmente considerado):</strong>&nbsp;todas as pessoas naturais que utilizarem o servi&ccedil;o Di&aacute;rio Oficial da Uni&atilde;o (DOU).</p>

//<p><strong>Leitura em texto:</strong>&nbsp;modo de consulta ao conte&uacute;do do DOU que permite acesso individualizado aos atos publicados, em formato html.</p>

//<p><strong>Vers&atilde;o Certificada:</strong>&nbsp;modo de consulta ao conte&uacute;do do DOU que permite acesso &agrave; p&aacute;gina da edi&ccedil;&atilde;o contendo o ato pesquisado, certificada digitalmente, em formato pdf.</p>

//<h4>4. Quais s&atilde;o as leis e normativos aplic&aacute;veis a esse servi&ccedil;o?</h4>

//<p>- Decreto n&ordm; 8.777, de 11 de maio de 2016: Institui a Pol&iacute;tica de Dados Abertos do Poder Executivo federal.</p>

//<p>- Decreto n&ordm; 9.215, de 29 de novembro de 2017: Disp&otilde;e sobre a publica&ccedil;&atilde;o do Di&aacute;rio Oficial da Uni&atilde;o.</p>

//<p>- Portaria IN/SG/PR n&ordm; 9, de 4 de fevereiro de 2021: Disp&otilde;e sobre publica&ccedil;&atilde;o de atos no Di&aacute;rio Oficial da Uni&atilde;o.</p>

//<h4>5. Descri&ccedil;&atilde;o do servi&ccedil;o</h4>

//<p>Com o objetivo facilitar o acesso imediato aos atos oficiais publicados no Di&aacute;rio Oficial da Uni&atilde;o (DOU), a Imprensa Nacional da Secretaria-Geral da Presid&ecirc;ncia da Rep&uacute;blica disponibiliza gratuitamente o aplicativo DOU, que permite acesso ao conte&uacute;do da edi&ccedil;&atilde;o do dia, busca por edi&ccedil;&otilde;es passadas e ainda: filtros de leitura por &oacute;rg&atilde;o e/ou tipo de ato; sele&ccedil;&atilde;o</p>

//<p>de leitura preferencial di&aacute;ria; possibilidade de &ldquo;favoritar&rdquo;, salvar no dispositivo e compartilhar as mat&eacute;rias; e baixar a vers&atilde;o em pdf.</p>

//<h4>6. Quais s&atilde;o as obriga&ccedil;&otilde;es dos usu&aacute;rios que utilizam o servi&ccedil;o?</h4>

//<p>O Usu&aacute;rio &eacute; respons&aacute;vel pela repara&ccedil;&atilde;o de todos e quaisquer danos, diretos ou indiretos (inclusive decorrentes de viola&ccedil;&atilde;o de quaisquer direitos de outros usu&aacute;rios, de terceiros, inclusive direitos de propriedade intelectual, de sigilo e de personalidade), que sejam causados &agrave; Imprensa Nacional, a qualquer outro Usu&aacute;rio, ou, ainda, a qualquer terceiro, inclusive em virtude do descumprimento do disposto nestes Termos de Uso e Pol&iacute;tica de Privacidade ou de qualquer ato praticado a partir da utiliza&ccedil;&atilde;o do servi&ccedil;o.</p>

//<p>A Imprensa Nacional n&atilde;o poder&aacute; ser responsabilizada pelos seguintes fatos:</p>

//<p>a. Equipamento infectado ou invadido por atacantes;</p>

//<p>b. Equipamento avariado no momento do consumo de servi&ccedil;os;</p>

//<p>c. Prote&ccedil;&atilde;o do equipamento;</p>

//<p>d. Prote&ccedil;&atilde;o das informa&ccedil;&otilde;es baseadas nos equipamentos dos usu&aacute;rios; e. Abuso de uso dos equipamentos dos usu&aacute;rios;</p>

//<p>f. Monitora&ccedil;&atilde;o clandestina do equipamento dos usu&aacute;rios;</p>

//<p>g. Vulnerabilidades ou instabilidades existentes nos sistemas dos usu&aacute;rios;</p>

//<p>O uso comercial das express&otilde;es utilizadas em aplicativos como marca, nome empresarial ou nome de dom&iacute;nio, al&eacute;m dos conte&uacute;dos do servi&ccedil;o, assim como os programas, bancos de dados, redes e arquivos est&atilde;o protegidos pelas leis e tratados internacionais de direito autoral, marcas, patentes, modelos e desenhos industriais.</p>

//<p>Ao acessar o aplicativo, os usu&aacute;rios declaram que ir&atilde;o respeitar todos os direitos de propriedade intelectual e os decorrentes da prote&ccedil;&atilde;o de marcas, patentes e/ou desenhos industriais, depositados ou registrados em, bem como todos os direitos referentes a terceiros que porventura estejam, ou estiverem de alguma forma, dispon&iacute;veis no servi&ccedil;o. O simples acesso ao servi&ccedil;o n&atilde;o confere aos usu&aacute;rios qualquer direito ao uso dos nomes, t&iacute;tulos, palavras, frases, marcas, patentes, imagens, dados e informa&ccedil;&otilde;es, dentre outras, que nele estejam ou estiverem dispon&iacute;veis.</p>

//<p>A reprodu&ccedil;&atilde;o de conte&uacute;do descritos anteriormente est&aacute; proibida, salvo com pr&eacute;via autoriza&ccedil;&atilde;o por escrito ou caso se destinem ao uso exclusivamente pessoal e sem que em nenhuma circunst&acirc;ncia os usu&aacute;rios adquiram qualquer direito sobre esses conte&uacute;dos.</p>

//<p>&Eacute; vedada a utiliza&ccedil;&atilde;o do servi&ccedil;o para finalidades comerciais, publicit&aacute;rias ou qualquer outra que contrarie a finalidade para a qual foi concebido, conforme definido neste documento, sob pena de sujei&ccedil;&atilde;o &agrave;s san&ccedil;&otilde;es cab&iacute;veis na Lei n&ordm; 9.610/1998, que protege os direitos autorais no Brasil.</p>

//<p>Os visitantes e usu&aacute;rios assumem toda e qualquer responsabilidade, de car&aacute;ter civil e/ou criminal, pela utiliza&ccedil;&atilde;o indevida das informa&ccedil;&otilde;es, textos, gr&aacute;ficos, marcas, imagens, enfim, todo e qualquer direito de propriedade intelectual ou industrial do servi&ccedil;o.</p>

//<p>Em nenhuma hip&oacute;tese, a Imprensa Nacional ser&aacute; respons&aacute;vel pela instala&ccedil;&atilde;o no equipamento do Usu&aacute;rio ou de terceiros, de c&oacute;digos maliciosos (v&iacute;rus, trojans, malware, worm, bot, backdoor, spyware, rootkit, ou de quaisquer outros que venham a ser criados), em decorr&ecirc;ncia da navega&ccedil;&atilde;o na Internet pelo usu&aacute;rio.</p>

//<h4>7. Quais s&atilde;o as responsabilidades da Imprensa Nacional?</h4>

//<p>Publicar e informar ao Usu&aacute;rio as futuras altera&ccedil;&otilde;es a estes Termos de Uso e Pol&iacute;tica de Privacidade conforme o princ&iacute;pio da publicidade estabelecido no artigo 37, caput, da Constitui&ccedil;&atilde;o Federal.</p>

//<p>Em nenhuma hip&oacute;tese, a Imprensa Nacional ser&aacute; respons&aacute;vel pela instala&ccedil;&atilde;o no equipamento do Usu&aacute;rio ou de terceiros, de c&oacute;digos maliciosos (v&iacute;rus, trojans, malware, worm, bot, backdoor, spyware, rootkit, ou de quaisquer outros que venham a ser criados), em decorr&ecirc;ncia da navega&ccedil;&atilde;o na Internet pelo usu&aacute;rio.</p>

//<p>Em hip&oacute;tese alguma, o servi&ccedil;o e seus colaboradores responsabilizam-se por eventuais danos diretos, indiretos, emergentes, especiais, imprevistos ou multas causadas, em qualquer mat&eacute;ria de responsabilidade, seja contratual, objetiva ou civil (inclusive neglig&ecirc;ncia ou outras), decorrentes de qualquer forma de uso do servi&ccedil;o, mesmo que advertida a possibilidade de tais danos.</p>

//<p>O usu&aacute;rio concorda que n&atilde;o usar&aacute; rob&ocirc;s, sistemas de varredura e armazenamento de dados (como &ldquo;spiders&rdquo; ou &ldquo;scrapers&rdquo;), links escondidos ou qualquer outro recurso escuso, ferramenta, programa, algoritmo ou m&eacute;todo coletor/extrator de dados autom&aacute;tico para acessar, adquirir, copiar ou monitorar o servi&ccedil;o, sem permiss&atilde;o expressa por escrito do &oacute;rg&atilde;o.</p>

//<p>Em se tratando de aplicativos em dispositivos m&oacute;veis sua comercializa&ccedil;&atilde;o &eacute; expressamente proibida. Ao concordar com este Termo de Uso e utilizar o aplicativo m&oacute;vel, o usu&aacute;rio receber&aacute; uma permiss&atilde;o do &oacute;rg&atilde;o para uso n&atilde;o comercial dos servi&ccedil;os oferecidos pelo aplicativo, o que, em nenhuma hip&oacute;tese, far&aacute; dele propriet&aacute;rio do aplicativo m&oacute;vel.</p>

//<p>Caso o usu&aacute;rio descumpra o Termo de Uso ou a Pol&iacute;tica de Privacidade, ou seja investigado em raz&atilde;o de m&aacute; conduta, este dever&aacute; responder legalmente por essa conduta.</p>

//<p>A Imprensa Nacional poder&aacute;, quanto &agrave;s ordens judiciais de pedido de informa&ccedil;&otilde;es, compartilhar informa&ccedil;&otilde;es necess&aacute;rias para investiga&ccedil;&otilde;es ou tomar medidas relacionadas a atividades ilegais, suspeitas de fraude ou amea&ccedil;as potenciais contra pessoas, bens ou sistemas que sustentam o servi&ccedil;o ou de outra forma necess&aacute;ria para cumprir com as obriga&ccedil;&otilde;es legais.</p>

//<p>A Imprensa Nacional se compromete a preservar a funcionalidade do servi&ccedil;o ou aplicativo, utilizando um layout que respeite a usabilidade e navegabilidade, facilitando a navega&ccedil;&atilde;o sempre que poss&iacute;vel, e exibir as funcionalidades de maneira completa, precisa e suficiente, de modo que as opera&ccedil;&otilde;es realizadas no servi&ccedil;o sejam claras.</p>

//<h4>8. Qual o contato pelo qual o usu&aacute;rio do servi&ccedil;o pode tirar suas d&uacute;vidas?</h4>

//<p>Caso o usu&aacute;rio tenha alguma d&uacute;vida sobre este Termo de Uso, ele poder&aacute; entrar em contato pelo e-mail&nbsp;<a href='mailto:caci@in.gov.br.>caci@in.gov.br.</a></p>",
//                  Active = true,
//                  TypeTerm = TypeTerm.TermUseProfessional,
//                  Version = 1
//              }
//              );
        }
    }
}
