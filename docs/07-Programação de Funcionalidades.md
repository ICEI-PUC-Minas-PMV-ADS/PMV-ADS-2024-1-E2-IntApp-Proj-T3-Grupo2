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


# Instruções de acesso

Site para visualização do que está sendo produzido até então:


https://padrinly-706d3ee3630c.herokuapp.com/


Cada tipo de usuário terá a sua própria visualização dentro do site, sendo os tipos criados até o momento:

- Administrador do site
- Instituição
- Padrinho/doador

Tela de Login:
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/1d8e2619-9608-4d33-859d-dc682e3d3530)


Nota: para cada usuário, a fim de testarmos as funcionalidades em desenvolvimento, criamos perfis fictícios, descritos abaixo.

### Visão do administrador:

Permite ver, editar e excluir Instituições:


login: _admin@admin.com_ <br>
senha: _123@mudar_ <br>
("m" minúsculo)


![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/7a2cdd2d-d9fe-433f-88e2-bee559ca502e)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/0bb0d9bb-c3b1-43e2-8225-98c7c3e20ba6)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/95bfb79d-11fd-445e-ad64-9b7cc11500bf)



#### Visão da Instituição:

Permite ver, editar e excluir padrinhos e afilhados:


login: _vidafeliz@email.com_ <br>
senha: _123@Mudar_ <br>
("M" maiúsculo)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/21ba9c0b-e993-4a64-9953-6e0dafc4fcfc)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/ebe0d1aa-76de-4f6f-b48e-71ddd2b4dd51)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/01c5bfe6-5e5d-41bf-ad83-20de13fc3dae)



![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/55f2f165-9aba-4f70-8e5e-09784f8c9934)


### Visão do padrinho/doador:


Os perfis das instituições serão criados pelo administrador da aplicação. Os perfis de afilhados e responsáveis serão criados pela instituição em que são vinculados. O padrinho/doador será o único perfil que poderá realizar a seu registro diretamente pelo site, sem passar por intermediadores:

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/767e7ec7-0068-4581-9f26-d9de627a7f49)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/56fb6942-dde0-4b3d-adb1-8705f9517e8a)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/d224d590-90a9-4bb9-98f9-8a9dc1bf34fa)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/70ff244f-8a02-42d5-bfad-dce40beceb19)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/b66b9264-6a32-4b83-ad6d-913b7f1f069e)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/d06721ae-176b-405d-833f-d9c87e8791a6)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/381593f5-46bf-4c2f-99ac-5bd07bcb2b62)


Também há um perfil fictício para teste:

login: _eduardosilva@email.com_ <br>
senha: _123@Mudar_ <br>
("M" maiúsculo)


### Visão do afilhado:


Permite ao afilhado interagir com a plataforma e realizar postagens para seu padrinho e compartilhar o que decidir ser interessante.

login: _lucassilva@email.com_ <br>
senha: _123@Mudar_ <br>
("M" maiúsculo)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/de18e65b-4df2-4c6b-9867-427780219042)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/1310828f-aa74-41b7-8de2-453892ad9093)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/PMV-ADS-2024-1-E2-IntApp-Proj-T3-Grupo2-Padrin.ly/assets/110932147/550ed4ae-7f7c-4a1e-abf5-1cce21f9cefe)











