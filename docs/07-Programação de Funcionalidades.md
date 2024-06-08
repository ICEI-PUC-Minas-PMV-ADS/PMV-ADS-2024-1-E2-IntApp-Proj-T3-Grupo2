# Programação de Funcionalidades

As Funcionalidades produzidas até o momento **(ETAPA 4 - 09/06/2024)** foram:


|ID    | Descrição do Requisito  | Artefato(s) produzido(s) |
|------|-----------------------------------------|----|
|RF-001| O usuário Administrador cadastrará Instituição (pessoa juridica) | Create.cshtml |
|RF-002| O usuário Instituição cadastrará seus alunos (pessoa fisica) | InstitutionIndex.cshtml |
|RF-003| O usuário doador/apadrinhador (pessoa fisica) se cadastrará no site | CreatePatron.cshtml / PersonController.cs / controllertarefas.cs | 
|RF-004| O usuário irá efetuar login no site   | ExternalLogins.cshtml  |
|RF-005| As instituições cadastradas devem ter a capacidade de gerenciar alunos vinculados a elas | InstitutionIndex.cshtml
|RF-006| O administrador do site deve ter a capacidade de gerenciar as instituições vinculadas | InstitutionIndex.cshtml |
|RF-007| O usuário aluno ira publicar textos e/ou imagens em seu mural | Person/Details.cshtml |



|ID    | Descrição do Requisito  | Artefato(s) produzido(s) |
|------|-----------------------------------------|----|
RNF-001|	Garantir que o sistema seja rápido e responsivo, mesmo durante períodos de alto tráfego ou carga intensiva de dados. | atualizar |
RNF-002|Projetar o sistema de forma modular e fácil de manter, permitindo atualizações e correções de bugs sem interrupções significativas no serviço.| atualizar |


# Instruções de acesso

Site para visualização do que está sendo produzido até então:


https://padrinly-706d3ee3630c.herokuapp.com/


Cada tipo de usuário terá a sua própria visualização dentro do site, sendo os tipos criados até o momento:

- Administrador do site
- Instituição
- Padrinho/doador

Tela de Login:
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/c9d4ff40-f0e9-423b-9491-795aa1ae922e)


Nota: para cada usuário, a fim de testarmos as funcionalidades em desenvolvimento, criamos perfis fictícios, descritos abaixo.

### Visão do administrador:

Permite ver, editar e excluir Instituições:


login: _admin@admin.com_ <br>
senha: _123@mudar_ <br>
("m" minúsculo)

Ir em "instituições":

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/14c39dfc-9947-45cc-893c-76f05b1b82a0)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/518ef575-96e4-44d3-896e-caac95b91275)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/ba6adaa0-fa36-427d-a14e-b153a1bf6720)


#### Visão da Instituição:

Permite ver, editar e excluir padrinhos e afilhados:


login: _vidafeliz@email.com_ <br>
senha: _123@Mudar_ <br>
("M" maiúsculo)

Ir em "Pessoa":

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/da5916f7-8350-4b97-a4bb-d23130812752)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/8b6bc814-6fbb-4b3f-bd05-c2f9a1c2478c)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/52e57037-c279-4a01-b9e4-e319c824e8c4)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/0138a1c5-533c-478f-a95f-fb019c31ddeb)



### Visão do padrinho/doador:


Os perfis das instituições serão criados pelo administrador da aplicação. Os perfis de afilhados e responsáveis serão criados pela instituição em que são vinculados. O padrinho/doador será o único perfil que poderá realizar a seu registro diretamente pelo site, sem passar por intermediadores:

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/39f26ad2-260a-45ee-bd26-383aae5b63d9)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/c690f4ed-1985-45ea-b4d3-88c0f005dd45)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/4147b883-e0ac-49eb-90ea-91d83d2d230a)


Também há um perfil fictício para teste:

login: _eduardosilva@email.com_ <br>
senha: _123@Mudar_ <br>
("M" maiúsculo)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/d8e91d12-18c1-46fe-8bbc-da39f35ef8a0)










