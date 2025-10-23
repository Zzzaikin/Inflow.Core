--
-- PostgreSQL database dump
--

\restrict Ugdb3EcEXH7Ru86qvaFhJDuNkdySxZ66Rjx92eDAblN1mOFKaaWnos9xBHGqBDf

-- Dumped from database version 16.10
-- Dumped by pg_dump version 16.10

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS "TestInflow";
--
-- Name: TestInflow; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "TestInflow" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'ru_RU.UTF-8';


ALTER DATABASE "TestInflow" OWNER TO postgres;

\unrestrict Ugdb3EcEXH7Ru86qvaFhJDuNkdySxZ66Rjx92eDAblN1mOFKaaWnos9xBHGqBDf
\connect "TestInflow"
\restrict Ugdb3EcEXH7Ru86qvaFhJDuNkdySxZ66Rjx92eDAblN1mOFKaaWnos9xBHGqBDf

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Contact; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Contact" (
    "Id" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" text NOT NULL
);


ALTER TABLE public."Contact" OWNER TO postgres;

--
-- Name: Order; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Order" (
    "Id" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "Userid" uuid NOT NULL
);


ALTER TABLE public."Order" OWNER TO postgres;

--
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "Id" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" text NOT NULL,
    "Createdon" timestamp without time zone DEFAULT timezone('utc'::text, CURRENT_TIMESTAMP) NOT NULL,
    "Modifiedon" timestamp without time zone DEFAULT timezone('utc'::text, CURRENT_TIMESTAMP),
    "Contactid" uuid NOT NULL,
    "Active" boolean
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- Data for Name: Contact; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Contact" ("Id", "Name") VALUES ('1fd2eea6-7314-4bf2-9e70-e4d585c9a537', 'Ivan');
INSERT INTO public."Contact" ("Id", "Name") VALUES ('259ef6a1-1f14-4f7d-a066-4d812b028200', 'Yura');
INSERT INTO public."Contact" ("Id", "Name") VALUES ('90f94dd0-4951-4257-abef-5ff9f66a3092', 'Dima');
INSERT INTO public."Contact" ("Id", "Name") VALUES ('5b1c0538-d7c2-41d6-9749-ae4454157793', 'Alexey');


--
-- Data for Name: Order; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('1b9e6672-3c27-4a58-ae18-beb3c79c0cdd', 'code1', NULL, '60cada01-51f2-4472-b778-2a5321b34cf2');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('662bd9ed-4c4a-4173-9022-e98bfc85034e', 'code2', NULL, '2cf5a835-f044-4087-81df-6b3353e419fb');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('4139f183-9340-4bec-9a16-59a982e7d3c6', 'code3', NULL, '5369c0c6-1707-46a5-a72f-33e0c4bb3433');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('3915a9b0-0a0f-4cd3-a6ef-1497f63c7c3c', 'code1', NULL, '60cada01-51f2-4472-b778-2a5321b34cf2');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('b2f3e8e2-7906-44ce-a8f1-dbf87999a6d5', 'code2', NULL, '2cf5a835-f044-4087-81df-6b3353e419fb');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('943457c9-0359-4298-9b91-c526f21b351a', 'code3', NULL, '5369c0c6-1707-46a5-a72f-33e0c4bb3433');
INSERT INTO public."Order" ("Id", "Name", "Description", "Userid") VALUES ('ea4fb850-5e93-48f3-8e84-0d2bc96c2b6f', 'code4', NULL, '7e283565-9d9c-4ab4-85a6-75301ed32c7e');


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."User" ("Id", "Name", "Createdon", "Modifiedon", "Contactid", "Active") VALUES ('60cada01-51f2-4472-b778-2a5321b34cf2', 'user1', '2025-10-22 19:15:27.935505', '2025-10-22 19:15:27.935505', '1fd2eea6-7314-4bf2-9e70-e4d585c9a537', NULL);
INSERT INTO public."User" ("Id", "Name", "Createdon", "Modifiedon", "Contactid", "Active") VALUES ('2cf5a835-f044-4087-81df-6b3353e419fb', 'user2', '2025-10-22 19:15:27.965799', '2025-10-22 19:15:27.965799', '259ef6a1-1f14-4f7d-a066-4d812b028200', NULL);
INSERT INTO public."User" ("Id", "Name", "Createdon", "Modifiedon", "Contactid", "Active") VALUES ('5369c0c6-1707-46a5-a72f-33e0c4bb3433', 'user3', '2025-10-22 19:15:27.973497', '2025-10-22 19:15:27.973497', '90f94dd0-4951-4257-abef-5ff9f66a3092', NULL);
INSERT INTO public."User" ("Id", "Name", "Createdon", "Modifiedon", "Contactid", "Active") VALUES ('7e283565-9d9c-4ab4-85a6-75301ed32c7e', 'user4', '2025-10-22 19:15:27.979562', '2025-10-22 19:15:27.979562', '5b1c0538-d7c2-41d6-9749-ae4454157793', NULL);


--
-- Name: Contact contact_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Contact"
    ADD CONSTRAINT contact_pk PRIMARY KEY ("Id");


--
-- Name: Order order_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT order_pk PRIMARY KEY ("Id");


--
-- Name: User user_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT user_pk PRIMARY KEY ("Id");


--
-- Name: Order order_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT order_user_id_fk FOREIGN KEY ("Userid") REFERENCES public."User"("Id");


--
-- Name: User user_contact_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT user_contact_id_fk FOREIGN KEY ("Contactid") REFERENCES public."Contact"("Id");


--
-- PostgreSQL database dump complete
--

\unrestrict Ugdb3EcEXH7Ru86qvaFhJDuNkdySxZ66Rjx92eDAblN1mOFKaaWnos9xBHGqBDf

