SELECT
a.animal AS animal1,
b.animal AS animal2
FROM ZooData a
JOIN ZooData b ON a.namberroom = b.namberroom
WHERE a.animal != b.animal;