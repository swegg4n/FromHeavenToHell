F�r att styra b�da karakt�rerna beh�vs minst en handkontroll ikoplad. Man m�ste manuellt st�lla in bool v�rdet PlayerDemonUsingMouse och PlayerAngelUsingMouse p� GameManager-gameobjektet under playermanager skriptet.
F�r att �ndra en ability drar du in dens scriptableobject (aoeClose01, New Dash Ability eller proj01) p� r�tt SerializedField i r�tt prefab (PlayerAngel, PlayerDemon eller CorruptedBunnyDog).
Ability:nas v�rden �ndras p� scritableobjectet i inspectorn.
Spelarnas Health �ndras GameManager-gameobjektet under playermanager skriptet.
Fiendernas Health �ndras i deras prefab under enemybaseclass-scriptet.